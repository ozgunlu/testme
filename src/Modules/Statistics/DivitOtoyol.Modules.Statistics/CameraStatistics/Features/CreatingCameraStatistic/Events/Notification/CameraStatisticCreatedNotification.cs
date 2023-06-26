using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Features.CreatingCameraStatistic.Events.Domain;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Features.CreatingCameraStatistic.Events.Notification;

public record CameraStatisticCreatedNotification
    (CameraStatisticCreated DomainEvent) : DomainNotificationEventWrapper<CameraStatisticCreated>(DomainEvent)
{
    public long Id => DomainEvent.CameraStatistic.Id;
    public long LocationId => DomainEvent.CameraStatistic.LocationInformation.Id;
    public string LocationName => DomainEvent.CameraStatistic.LocationInformation.Name;
    public long CameraId => DomainEvent.CameraStatistic.CameraInformation.Id;
    public string CameraName => DomainEvent.CameraStatistic.CameraInformation.Name;
    public long TypeId => DomainEvent.CameraStatistic.TypeInformation.Id;
    public string TypeName => DomainEvent.CameraStatistic.TypeInformation.Name;
    public long MakeId => DomainEvent.CameraStatistic.MakeInformation.Id;
    public string MakeName => DomainEvent.CameraStatistic.MakeInformation.Name;
    public long ModelId => DomainEvent.CameraStatistic.ModelInformation.Id;
    public string ModelName => DomainEvent.CameraStatistic.ModelInformation.Name;
    public long ColorId => DomainEvent.CameraStatistic.ColorInformation.Id;
    public string ColorName => DomainEvent.CameraStatistic.ColorInformation.Name;
    public string Plate => DomainEvent.CameraStatistic.Plate;
    public long TotalPassages => DomainEvent.CameraStatistic.TotalPassages;
    public DateTime FirstSeenAt => DomainEvent.CameraStatistic.FirstSeenAt;
    public DateTime LastSeenAt => DomainEvent.CameraStatistic.LastSeenAt;
}

internal class CameraStatisticCreatedNotificationHandler : IDomainNotificationEventHandler<CameraStatisticCreatedNotification>
{
    private readonly IBus _bus;

    public CameraStatisticCreatedNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(CameraStatisticCreatedNotification notification, CancellationToken cancellationToken)
    {
        // We could publish integration event to bus here
        // await _bus.PublishAsync(
        //     new ECommerce.Modules.Shared.Catalogs.Products.Events.Integration.ProductCreated(
        //         notification.Id,
        //         notification.Name,
        //         notification.Stock,
        //         notification.CategoryName ?? "",
        //         notification.Stock),
        //     null,
        //     cancellationToken);

        return;
    }
}
