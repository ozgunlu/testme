using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Cameras.Cameras.Exceptions.Application;
using DivitOtoyol.Modules.Cameras.Cameras.ValueObjects;
using DivitOtoyol.Modules.Cameras.Locations.Exceptions;
using DivitOtoyol.Modules.Cameras.Shared.Contracts;
using DivitOtoyol.Modules.Cameras.Shared.Extensions;
using DivitOtoyol.Modules.Cameras.Shared.Location;
using FluentValidation;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.UpdatingCamera;

public record UpdateCamera(
    CameraId Id,
    long LocationId,
    string Name,
    string? BiosName = null,
    string? SerialNumber = null,
    string? Ip = null) : ITxUpdateCommand;

internal class UpdateCameraValidator : AbstractValidator<UpdateCamera>
{
    public UpdateCameraValidator()
    {
        RuleFor(x => x.Id)
            .Must(id => id.Value > 0)
            .WithMessage("CameraId must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.LocationId)
            .NotEmpty().WithMessage("Location Id is required.");
    }
}

internal class UpdateCameraCommandHandler : ICommandHandler<UpdateCamera>
{
    private readonly ICameraDbContext _cameraDbContext;
    private readonly ILocationGrpcClient _locationGrpcClient;

    public UpdateCameraCommandHandler(
        ICameraDbContext cameraDbContext,
        ILocationGrpcClient locationGrpcClient)
    {
        _cameraDbContext = cameraDbContext;
        _locationGrpcClient = locationGrpcClient;
    }

    public async Task<Unit> Handle(UpdateCamera command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var camera = await _cameraDbContext.FindCameraAsync(command.Id);
        Guard.Against.NotFound(camera, new CameraNotFoundException(command.Id));

        var location = (await _locationGrpcClient.GetLocationByIdAsync(command.LocationId, cancellationToken));
        Guard.Against.NotFound(location, new LocationNotFoundException(command.LocationId));

        var locationInformation = LocationInformation.Create(location!.Id, location.Name);

        camera!.ChangeLocationInformation(locationInformation);
        camera.ChangeName(command.Name);
        camera.ChangeBiosName(command.BiosName);
        camera.ChangeSerialNumber(command.SerialNumber);
        camera.ChangeIp(command.Ip);

        await _cameraDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
