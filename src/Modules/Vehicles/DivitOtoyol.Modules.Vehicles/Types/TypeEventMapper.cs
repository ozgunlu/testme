using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using DivitOtoyol.Modules.Vehicles.Types.Features.CreatingType.Events.Domain;
using DivitOtoyol.Modules.Vehicles.Types.Features.CreatingType.Events.Notification;

namespace DivitOtoyol.Modules.Vehicles.Types;

public class TypeEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            TypeCreated e =>
                new Features.CreatingType.Events.Integration.TypeCreated(
                    e.VehicleType.Id,
                    e.VehicleType.Name),
            _ => null
        };
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            TypeCreated e => new TypeCreatedNotification(e),
            _ => null
        };
    }

    public IReadOnlyList<IIntegrationEvent?> MapToIntegrationEvents(IReadOnlyList<IDomainEvent> domainEvents)
    {
        return domainEvents.Select(MapToIntegrationEvent).ToList().AsReadOnly();
    }

    public IReadOnlyList<IDomainNotificationEvent?> MapToDomainNotificationEvents(
        IReadOnlyList<IDomainEvent> domainEvents)
    {
        return domainEvents.Select(MapToDomainNotificationEvent).ToList().AsReadOnly();
    }
}
