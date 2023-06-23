using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Colors.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.UpdatingColor;

public record ColorUpdated(Color Color) : DomainEvent;

public class ColorUpdatedHandler : IDomainEventHandler<ColorUpdated>
{
    private readonly VehicleDbContext _dbContext;

    public ColorUpdatedHandler(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(ColorUpdated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
