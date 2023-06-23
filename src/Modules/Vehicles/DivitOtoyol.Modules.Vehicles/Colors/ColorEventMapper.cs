using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using DivitOtoyol.Modules.Vehicles.Colors.Features.CreatingColor.Events.Domain;
using DivitOtoyol.Modules.Vehicles.Colors.Features.CreatingColor.Events.Notification;

namespace DivitOtoyol.Modules.Vehicles.Colors;

public class ColorEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            ColorCreated e =>
                new Features.CreatingColor.Events.Integration.ColorCreated(
                    e.Color.Id,
                    e.Color.Name),
            _ => null
        };
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            ColorCreated e => new ColorCreatedNotification(e),
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
