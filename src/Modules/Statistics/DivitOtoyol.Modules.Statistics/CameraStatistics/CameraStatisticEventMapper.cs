using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Features.CreatingCameraStatistic.Events.Domain;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Features.CreatingCameraStatistic.Events.Notification;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics;

public class CameraStatisticEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            CameraStatisticCreated e =>
                new Features.CreatingCameraStatistic.Events.Integration.CameraStatisticCreated(
                    e.CameraStatistic.Id,
                    e.CameraStatistic.LocationInformation.Id,
                    e.CameraStatistic.LocationInformation.Name,
                    e.CameraStatistic.CameraInformation.Id,
                    e.CameraStatistic.CameraInformation.Name,
                    e.CameraStatistic.TypeInformation.Id,
                    e.CameraStatistic.TypeInformation.Name,
                    e.CameraStatistic.MakeInformation.Id,
                    e.CameraStatistic.MakeInformation.Name,
                    e.CameraStatistic.ModelInformation.Id,
                    e.CameraStatistic.ModelInformation.Name,
                    e.CameraStatistic.ColorInformation.Id,
                    e.CameraStatistic.ColorInformation.Name,
                    e.CameraStatistic.Plate,
                    e.CameraStatistic.TotalPassages,
                    e.CameraStatistic.FirstSeenAt,
                    e.CameraStatistic.LastSeenAt),
            _ => null
        };
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            CameraStatisticCreated e => new CameraStatisticCreatedNotification(e),
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
