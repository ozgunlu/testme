using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Locations.Locations.Features.CreatingLocation.Events.Domain;

namespace DivitOtoyol.Modules.Locations.Locations.Features.CreatingLocation.Events.Notification;

public record LocationCreatedNotification
    (LocationCreated DomainEvent) : DomainNotificationEventWrapper<LocationCreated>(DomainEvent)
{
    public long Id => DomainEvent.Location.Id;
    public string Name => DomainEvent.Location.Name;
}

internal class LocationCreatedNotificationHandler : IDomainNotificationEventHandler<LocationCreatedNotification>
{
    private readonly IBus _bus;

    public LocationCreatedNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(LocationCreatedNotification notification, CancellationToken cancellationToken)
    {
        return;
    }
}
