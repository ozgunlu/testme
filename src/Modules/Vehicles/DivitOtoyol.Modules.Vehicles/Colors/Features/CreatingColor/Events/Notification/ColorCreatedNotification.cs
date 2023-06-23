using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Colors.Features.CreatingColor.Events.Domain;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.CreatingColor.Events.Notification;

public record ColorCreatedNotification
    (ColorCreated DomainEvent) : DomainNotificationEventWrapper<ColorCreated>(DomainEvent)
{
    public long Id => DomainEvent.Color.Id;
    public string Name => DomainEvent.Color.Name;
}

internal class ColorCreatedNotificationHandler : IDomainNotificationEventHandler<ColorCreatedNotification>
{
    private readonly IBus _bus;

    public ColorCreatedNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(ColorCreatedNotification notification, CancellationToken cancellationToken)
    {
        return;
    }
}
