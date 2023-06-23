using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Models.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.UpdatingModel;

public record ModelUpdated(Model Model) : DomainEvent;

public class ModelUpdatedHandler : IDomainEventHandler<ModelUpdated>
{
    private readonly VehicleDbContext _dbContext;

    public ModelUpdatedHandler(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(ModelUpdated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
