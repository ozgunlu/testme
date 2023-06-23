using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.PlateRecognitions.Records.Models.Write;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Data;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Events.Domain;

public record RecordCreated(Record Record) : DomainEvent;

internal class RecordCreatedHandler : IDomainEventHandler<RecordCreated>
{
    private readonly PlateRecognitionDbContext _dbContext;

    public RecordCreatedHandler(PlateRecognitionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(RecordCreated notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
    }
}

// Mapping domain event to integration event in domain event handler is better from mapping in command handler (for preserving our domain rule invariants).
internal class RecordCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<RecordCreated>
{
    public RecordCreatedDomainEventToIntegrationMappingHandler()
    {
    }

    public Task Handle(RecordCreated domainEvent, CancellationToken cancellationToken)
    {
        // 1. Mapping DomainEvent To IntegrationEvent
        // 2. Save Integration Event to Outbox
        return Task.CompletedTask;
    }
}
