using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Makes.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.CreatingMake.Events.Domain;

public record MakeCreated(Make Make) : DomainEvent;

internal class MakeCreatedHandler : IDomainEventHandler<MakeCreated>
{
    private readonly VehicleDbContext _dbContext;

    public MakeCreatedHandler(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(MakeCreated notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
    }
}

internal class MakeCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<MakeCreated>
{
    public MakeCreatedDomainEventToIntegrationMappingHandler()
    {
    }

    public Task Handle(MakeCreated domainEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
