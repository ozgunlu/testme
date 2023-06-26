using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Models;
using DivitOtoyol.Modules.Statistics.Shared.Data;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Features.CreatingCameraStatistic.Events.Domain;

public record CameraStatisticCreated(CameraStatistic CameraStatistic) : DomainEvent;

internal class CameraStatisticCreatedHandler : IDomainEventHandler<CameraStatisticCreated>
{
    private readonly StatisticDbContext _dbContext;

    public CameraStatisticCreatedHandler(StatisticDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CameraStatisticCreated notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
    }
}

// Mapping domain event to integration event in domain event handler is better from mapping in command handler (for preserving our domain rule invariants).
internal class CameraStatisticCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<CameraStatisticCreated>
{
    public CameraStatisticCreatedDomainEventToIntegrationMappingHandler()
    {
    }

    public Task Handle(CameraStatisticCreated domainEvent, CancellationToken cancellationToken)
    {
        // 1. Mapping DomainEvent To IntegrationEvent
        // 2. Save Integration Event to Outbox
        return Task.CompletedTask;
    }
}
