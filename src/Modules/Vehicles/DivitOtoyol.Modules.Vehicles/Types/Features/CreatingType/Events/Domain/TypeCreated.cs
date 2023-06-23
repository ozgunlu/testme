using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using VehicleType = DivitOtoyol.Modules.Vehicles.Types.Models.Type;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.CreatingType.Events.Domain;

public record TypeCreated(VehicleType VehicleType) : DomainEvent;

internal class TypeCreatedHandler : IDomainEventHandler<TypeCreated>
{
    private readonly VehicleDbContext _dbContext;

    public TypeCreatedHandler(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(TypeCreated notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
    }
}

// Mapping domain event to integration event in domain event handler is better from mapping in command handler (for preserving our domain rule invariants).
internal class TypeCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<TypeCreated>
{
    public TypeCreatedDomainEventToIntegrationMappingHandler()
    {
    }

    public Task Handle(TypeCreated domainEvent, CancellationToken cancellationToken)
    {
        // 1. Mapping DomainEvent To IntegrationEvent
        // 2. Save Integration Event to Outbox
        return Task.CompletedTask;
    }
}
