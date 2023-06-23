using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Locations.Locations.Models;
using DivitOtoyol.Modules.Locations.Shared.Data;

namespace DivitOtoyol.Modules.Locations.Locations.Features.UpdatingLocation;

public record LocationUpdated(Location Location) : DomainEvent;

public class LocationUpdatedHandler : IDomainEventHandler<LocationUpdated>
{
    private readonly LocationDbContext _dbContext;

    public LocationUpdatedHandler(LocationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(LocationUpdated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
