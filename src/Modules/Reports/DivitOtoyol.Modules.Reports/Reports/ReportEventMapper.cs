using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using DivitOtoyol.Modules.Systems.Options.Features.CreatingOption.Events.Domain;
using DivitOtoyol.Modules.Systems.Options.Features.CreatingOption.Events.Notification;

namespace DivitOtoyol.Modules.Reports.Reports;

public class OptionEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            OptionCreated e =>
                new Features.CreatingOption.Events.Integration.OptionCreated(
                    e.Option.Id,
                    e.Option.Key,
                    e.Option.Value,
                    e.Option.Modules,
                    e.Option.CanUpdate,
                    e.Option.CanDelete),
            _ => null
        };
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            OptionCreated e => new OptionCreatedNotification(e),
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
