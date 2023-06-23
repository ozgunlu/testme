using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Cameras.Cameras.Models;
using DivitOtoyol.Modules.Cameras.Shared.Data;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.CreatingCamera.Events.Domain;

public record CameraCreated(Camera Camera) : DomainEvent;

internal class CameraCreatedHandler : IDomainEventHandler<CameraCreated>
{
    private readonly CameraDbContext _dbContext;

    public CameraCreatedHandler(CameraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CameraCreated notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
    }
}

internal class CameraCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<CameraCreated>
{
    public CameraCreatedDomainEventToIntegrationMappingHandler()
    {
    }

    public Task Handle(CameraCreated domainEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
