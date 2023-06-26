using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Cameras.Cameras.Dtos;
using DivitOtoyol.Modules.Cameras.Cameras.Models;
using DivitOtoyol.Modules.Cameras.Cameras.ValueObjects;
using DivitOtoyol.Modules.Cameras.Locations.Exceptions;
using DivitOtoyol.Modules.Cameras.Shared.Data;
using DivitOtoyol.Modules.Cameras.Shared.Location;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.CreatingCamera;

public record CreateCamera(
    long LocationId,
    string Name,
    string? BiosName = null,
    string? SerialNumber = null,
    string? Ip = null) : ITxCreateCommand<CreateCameraResponse>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

public class CreateCameraValidator : AbstractValidator<CreateCamera>
{
    public CreateCameraValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.LocationId)
            .NotEmpty();
    }
}

public class CreateCameraHandler : ICommandHandler<CreateCamera, CreateCameraResponse>
{
    private readonly CameraDbContext _cameraDbContext;
    private readonly ILocationApiClient _locationApiClient;
    private readonly ILogger<CreateCameraHandler> _logger;
    private readonly IMapper _mapper;

    public CreateCameraHandler(
        CameraDbContext cameraDbContext,
        ILocationApiClient locationApiClient,
        IMapper mapper,
        ILogger<CreateCameraHandler> logger)
    {
        _cameraDbContext = cameraDbContext;
        _locationApiClient = locationApiClient;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateCameraResponse> Handle(
        CreateCamera command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var location = (await _locationApiClient.GetLocationByIdAsync(command.LocationId, cancellationToken))?.Location;
        Guard.Against.NotFound(location, new LocationNotFoundException(command.LocationId));

        var locationInformation = LocationInformation.Create(location!.Id, location.Name);
        Console.WriteLine($"Location Information - Id: {locationInformation.Id}, Name: {locationInformation.Name}");

        var camera = Camera.Create(
            command.Id,
            locationInformation,
            command.Name,
            command.BiosName,
            command.SerialNumber,
            command.Ip);

        await _cameraDbContext.AddAsync(camera, cancellationToken: cancellationToken);

        await _cameraDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Camera a with ID: '{CameraId} created.'", command.Id);

        var cameraDto = _mapper.Map<CameraDto>(camera);

        return new CreateCameraResponse(cameraDto);
    }
}
