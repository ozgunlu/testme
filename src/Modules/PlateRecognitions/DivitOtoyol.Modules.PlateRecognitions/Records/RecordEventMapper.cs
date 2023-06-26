using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Events.Domain;
using DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Events.Notification;

namespace DivitOtoyol.Modules.PlateRecognitions.Records;

public class RecordEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            RecordCreated e =>
                new Features.CreatingRecord.Events.Integration.RecordCreated(
                    e.Record.Id,
                    e.Record.Plate,
                    e.Record.CameraInformation.Id,
                    e.Record.CameraInformation.Name,
                    e.Record.MakeInformation?.Id.Value,
                    e.Record.MakeInformation?.Name,
                    e.Record.ModelInformation?.Id.Value,
                    e.Record.ModelInformation?.Name,
                    e.Record.ColorInformation?.Id.Value,
                    e.Record.ColorInformation?.Name,
                    e.Record.LprDate),
            _ => null
        };
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            RecordCreated e => new RecordCreatedNotification(e),
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
