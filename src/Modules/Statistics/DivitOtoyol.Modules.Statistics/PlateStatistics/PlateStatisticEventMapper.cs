using BuildingBlocks.Abstractions.CQRS.Event;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Features.CreatingPlateStatistic.Events.Domain;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Features.CreatingPlateStatistic.Events.Notification;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics;

public class PlateStatisticEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            PlateStatisticCreated e =>
                new Features.CreatingPlateStatistic.Events.Integration.PlateStatisticCreated(
                    e.PlateStatistic.Id,
                    e.PlateStatistic.LocationInformation.Id,
                    e.PlateStatistic.LocationInformation.Name,
                    e.PlateStatistic.CameraInformation.Id,
                    e.PlateStatistic.CameraInformation.Name,
                    e.PlateStatistic.TypeInformation.Id,
                    e.PlateStatistic.TypeInformation.Name,
                    e.PlateStatistic.MakeInformation.Id,
                    e.PlateStatistic.MakeInformation.Name,
                    e.PlateStatistic.ModelInformation.Id,
                    e.PlateStatistic.ModelInformation.Name,
                    e.PlateStatistic.ColorInformation.Id,
                    e.PlateStatistic.ColorInformation.Name,
                    e.PlateStatistic.Plate,
                    e.PlateStatistic.TotalPassages,
                    e.PlateStatistic.FirstSeenAt,
                    e.PlateStatistic.LastSeenAt),
            _ => null
        };
    }

    public IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            PlateStatisticCreated e => new PlateStatisticCreatedNotification(e),
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
