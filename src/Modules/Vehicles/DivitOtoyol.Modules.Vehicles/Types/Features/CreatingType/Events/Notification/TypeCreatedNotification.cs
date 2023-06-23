using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Types.Features.CreatingType.Events.Domain;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.CreatingType.Events.Notification;

public record TypeCreatedNotification
    (TypeCreated DomainEvent) : DomainNotificationEventWrapper<TypeCreated>(DomainEvent)
{
    public long Id => DomainEvent.VehicleType.Id;
    public string Name => DomainEvent.VehicleType.Name;
}

internal class TypeCreatedNotificationHandler : IDomainNotificationEventHandler<TypeCreatedNotification>
{
    private readonly IBus _bus;

    public TypeCreatedNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(TypeCreatedNotification notification, CancellationToken cancellationToken)
    {
        return;
    }
}
