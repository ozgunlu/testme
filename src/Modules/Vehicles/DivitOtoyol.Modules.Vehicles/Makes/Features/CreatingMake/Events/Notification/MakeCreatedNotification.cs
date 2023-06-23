using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Makes.Features.CreatingMake.Events.Domain;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.CreatingMake.Events.Notification;

public record MakeCreatedNotification
    (MakeCreated DomainEvent) : DomainNotificationEventWrapper<MakeCreated>(DomainEvent)
{
    public long Id => DomainEvent.Make.Id;
    public string Name => DomainEvent.Make.Name;
}

internal class MakeCreatedNotificationHandler : IDomainNotificationEventHandler<MakeCreatedNotification>
{
    private readonly IBus _bus;

    public MakeCreatedNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(MakeCreatedNotification notification, CancellationToken cancellationToken)
    {
        return;
    }
}
