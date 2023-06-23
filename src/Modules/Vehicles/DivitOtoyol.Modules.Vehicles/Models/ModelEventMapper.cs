using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using DivitOtoyol.Modules.Vehicles.Models.Features.CreatingModel.Events.Domain;
using DivitOtoyol.Modules.Vehicles.Models.Features.CreatingModel.Events.Notification;

namespace DivitOtoyol.Modules.Vehicles.Models;

public class ModelEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            ModelCreated e =>
                new Features.CreatingModel.Events.Integration.ModelCreated(
                    e.Model.Id,
                    e.Model.Name),
            _ => null
        };
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            ModelCreated e => new ModelCreatedNotification(e),
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
