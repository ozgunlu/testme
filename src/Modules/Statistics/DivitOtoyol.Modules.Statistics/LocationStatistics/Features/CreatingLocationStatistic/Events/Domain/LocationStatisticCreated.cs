using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Models;
using DivitOtoyol.Modules.Statistics.Shared.Data;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.Features.CreatingLocationStatistic.Events.Domain;

public record LocationStatisticCreated(LocationStatistic LocationStatistic) : DomainEvent;

internal class LocationStatisticCreatedHandler : IDomainEventHandler<LocationStatisticCreated>
{
    private readonly StatisticDbContext _dbContext;

    public LocationStatisticCreatedHandler(StatisticDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(LocationStatisticCreated notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
    }
}

// Mapping domain event to integration event in domain event handler is better from mapping in command handler (for preserving our domain rule invariants).
internal class LocationStatisticCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<LocationStatisticCreated>
{
    public LocationStatisticCreatedDomainEventToIntegrationMappingHandler()
    {
    }

    public Task Handle(LocationStatisticCreated domainEvent, CancellationToken cancellationToken)
    {
        // 1. Mapping DomainEvent To IntegrationEvent
        // 2. Save Integration Event to Outbox
        return Task.CompletedTask;
    }
}
