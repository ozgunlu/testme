using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Cameras.Cameras.Models;
using DivitOtoyol.Modules.Cameras.Shared.Data;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.UpdatingCamera;

public record CameraUpdated(Camera Camera) : DomainEvent;

public class CameraUpdatedHandler : IDomainEventHandler<CameraUpdated>
{
    private readonly CameraDbContext _dbContext;

    public CameraUpdatedHandler(CameraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(CameraUpdated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
