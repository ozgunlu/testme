using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using DivitOtoyol.Modules.Servers.Servers.Features.CreatingServer.Events.Domain;
using DivitOtoyol.Modules.Servers.Servers.Features.CreatingServer.Events.Notification;

namespace DivitOtoyol.Modules.Servers.Servers;

public class ServerEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            ServerCreated e =>
                new Features.CreatingServer.Events.Integration.ServerCreated(
                    e.Server.Id,
                    e.Server.LocationInformation.Id,
                    e.Server.LocationInformation.Name,
                    e.Server.Name,
                    e.Server.Ip),
            _ => null
        };
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            ServerCreated e => new ServerCreatedNotification(e),
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
