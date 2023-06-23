using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Models.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.CreatingModel.Events.Domain;

public record ModelCreated(Model Model) : DomainEvent;

internal class ModelCreatedHandler : IDomainEventHandler<ModelCreated>
{
    private readonly VehicleDbContext _dbContext;

    public ModelCreatedHandler(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(ModelCreated notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
    }
}

internal class ModelCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<ModelCreated>
{
    public ModelCreatedDomainEventToIntegrationMappingHandler()
    {
    }

    public Task Handle(ModelCreated domainEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
