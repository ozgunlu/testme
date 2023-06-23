using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using DivitOtoyol.Modules.Vehicles.Makes.Features.CreatingMake.Events.Domain;
using DivitOtoyol.Modules.Vehicles.Makes.Features.CreatingMake.Events.Notification;

namespace DivitOtoyol.Modules.Vehicles.Makes;

public class MakeEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            MakeCreated e =>
                new Features.CreatingMake.Events.Integration.MakeCreated(
                    e.Make.Id,
                    e.Make.Name),
            _ => null
        };
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            MakeCreated e => new MakeCreatedNotification(e),
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
