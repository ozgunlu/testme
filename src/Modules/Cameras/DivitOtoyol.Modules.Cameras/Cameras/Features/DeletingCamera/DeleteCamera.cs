using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Cameras.Cameras.Exceptions.Application;
using DivitOtoyol.Modules.Cameras.Shared.Data;
using DivitOtoyol.Modules.Cameras.Shared.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.DeletingCamera;

public record DeleteCamera(long Id) : ITxCommand;

internal class DeleteCameraValidator : AbstractValidator<DeleteCamera>
{
    public DeleteCameraValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal class DeleteCameraHandler : ICommandHandler<DeleteCamera>
{
    private readonly CameraDbContext _cameraDbContext;
    private readonly ILogger<DeleteCameraHandler> _logger;

    public DeleteCameraHandler(
        CameraDbContext cameraDbContext,
        ILogger<DeleteCameraHandler> logger)
    {
        _cameraDbContext = cameraDbContext;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteCamera command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var camera = await _cameraDbContext.FindCameraAsync(command.Id);

        Guard.Against.NotFound(camera, new CameraNotFoundException(command.Id));

        _cameraDbContext.Cameras.Remove(camera!);

        await _cameraDbContext.SaveChangesAsync(cancellationToken);

        // for raising a deleted domain event
        camera!.Delete();

        _logger.LogInformation("Camera with id '{Id} removed.'", command.Id);

        return Unit.Value;
    }
}
