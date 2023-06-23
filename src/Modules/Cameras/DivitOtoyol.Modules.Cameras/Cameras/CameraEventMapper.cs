using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using DivitOtoyol.Modules.Cameras.Cameras.Features.CreatingCamera.Events.Domain;

namespace DivitOtoyol.Modules.Cameras.Cameras;

public class CameraEventMapper : IIntegrationEventMapper
{
    public IReadOnlyList<IIntegrationEvent?> MapToIntegrationEvents(IReadOnlyList<IDomainEvent> domainEvents)
    {
        return domainEvents.Select(MapToIntegrationEvent).ToList();
    }

    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            CameraCreated e =>
                new Features.CreatingCamera.Events.Integration.CameraCreated(
                    e.Camera.Id, e.Camera.Name),
            _ => null
        };
    }
}
