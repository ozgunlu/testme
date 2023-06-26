using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Models;
using DivitOtoyol.Modules.Statistics.Shared.Data;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.Features.CreatingPlateStatistic.Events.Domain;

public record PlateStatisticCreated(PlateStatistic PlateStatistic) : DomainEvent;

internal class PlateStatisticCreatedHandler : IDomainEventHandler<PlateStatisticCreated>
{
    private readonly StatisticDbContext _dbContext;

    public PlateStatisticCreatedHandler(StatisticDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(PlateStatisticCreated notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
    }
}

// Mapping domain event to integration event in domain event handler is better from mapping in command handler (for preserving our domain rule invariants).
internal class PlateStatisticCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<PlateStatisticCreated>
{
    public PlateStatisticCreatedDomainEventToIntegrationMappingHandler()
    {
    }

    public Task Handle(PlateStatisticCreated domainEvent, CancellationToken cancellationToken)
    {
        // 1. Mapping DomainEvent To IntegrationEvent
        // 2. Save Integration Event to Outbox
        return Task.CompletedTask;
    }
}
