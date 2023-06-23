using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Locations.Locations.Models;
using DivitOtoyol.Modules.Locations.Shared.Data;

namespace DivitOtoyol.Modules.Locations.Locations.Features.CreatingLocation.Events.Domain;

public record LocationCreated(Location Location) : DomainEvent;

internal class LocationCreatedHandler : IDomainEventHandler<LocationCreated>
{
    private readonly LocationDbContext _dbContext;

    public LocationCreatedHandler(LocationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(LocationCreated notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
    }
}

internal class LocationCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<LocationCreated>
{
    public LocationCreatedDomainEventToIntegrationMappingHandler()
    {
    }

    public Task Handle(LocationCreated domainEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
