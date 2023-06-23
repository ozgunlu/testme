using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using VehicleType = DivitOtoyol.Modules.Vehicles.Types.Models.Type;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.UpdatingType;

public record TypeUpdated(VehicleType VehicleType) : DomainEvent;

public class TypeUpdatedHandler : IDomainEventHandler<TypeUpdated>
{
    private readonly VehicleDbContext _dbContext;

    public TypeUpdatedHandler(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(TypeUpdated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
