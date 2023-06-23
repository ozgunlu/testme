using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Cameras.Cameras.Models;
using DivitOtoyol.Modules.Cameras.Shared.Data;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.DeletingCamera;

public record CameraDeleted(Camera Camera) : DomainEvent;

internal class CameraDeletedHandler : IDomainEventHandler<CameraDeleted>
{
    private readonly ICommandProcessor _commandProcessor;
    private readonly IMapper _mapper;
    private readonly CameraDbContext _cameraDbContext;

    public CameraDeletedHandler(
        ICommandProcessor commandProcessor,
        IMapper mapper,
        CameraDbContext cameraDbContext)
    {
        _commandProcessor = commandProcessor;
        _mapper = mapper;
        _cameraDbContext = cameraDbContext;
    }

    public Task Handle(CameraDeleted notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
