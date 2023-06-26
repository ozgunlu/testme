using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Features.CreatingLocationStatistic.Events.Domain;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Features.CreatingLocationStatistic.Events.Notification;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics;

public class LocationStatisticEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            LocationStatisticCreated e =>
                new Features.CreatingLocationStatistic.Events.Integration.LocationStatisticCreated(
                    e.LocationStatistic.Id,
                    e.LocationStatistic.LocationInformation.Id,
                    e.LocationStatistic.LocationInformation.Name,
                    e.LocationStatistic.CameraInformation.Id,
                    e.LocationStatistic.CameraInformation.Name,
                    e.LocationStatistic.TypeInformation.Id,
                    e.LocationStatistic.TypeInformation.Name,
                    e.LocationStatistic.MakeInformation.Id,
                    e.LocationStatistic.MakeInformation.Name,
                    e.LocationStatistic.ModelInformation.Id,
                    e.LocationStatistic.ModelInformation.Name,
                    e.LocationStatistic.ColorInformation.Id,
                    e.LocationStatistic.ColorInformation.Name,
                    e.LocationStatistic.Plate,
                    e.LocationStatistic.TotalPassages,
                    e.LocationStatistic.FirstSeenAt,
                    e.LocationStatistic.LastSeenAt),
            _ => null
        };
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            LocationStatisticCreated e => new LocationStatisticCreatedNotification(e),
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
