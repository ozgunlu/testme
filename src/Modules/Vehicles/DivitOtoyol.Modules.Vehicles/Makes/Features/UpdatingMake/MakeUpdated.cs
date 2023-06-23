using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Makes.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.UpdatingMake;

public record MakeUpdated(Make Make) : DomainEvent;

public class MakeUpdatedHandler : IDomainEventHandler<MakeUpdated>
{
    private readonly VehicleDbContext _dbContext;

    public MakeUpdatedHandler(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(MakeUpdated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
