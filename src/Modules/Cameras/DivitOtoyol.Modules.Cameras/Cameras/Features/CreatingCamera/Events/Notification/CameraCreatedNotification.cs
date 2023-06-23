using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Cameras.Cameras.Features.CreatingCamera.Events.Domain;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.CreatingCamera.Events.Notification;

public record CameraCreatedNotification
    (CameraCreated DomainEvent) : DomainNotificationEventWrapper<CameraCreated>(DomainEvent)
{
    public long Id => DomainEvent.Camera.Id;
    public string Name => DomainEvent.Camera.Name;
}

internal class CameraCreatedNotificationHandler : IDomainNotificationEventHandler<CameraCreatedNotification>
{
    private readonly IBus _bus;

    public CameraCreatedNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(CameraCreatedNotification notification, CancellationToken cancellationToken)
    {
        return;
    }
}
