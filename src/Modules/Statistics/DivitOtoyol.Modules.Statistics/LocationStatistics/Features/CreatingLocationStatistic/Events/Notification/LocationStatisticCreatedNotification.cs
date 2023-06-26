using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Features.CreatingLocationStatistic.Events.Domain;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.Features.CreatingLocationStatistic.Events.Notification;

public record LocationStatisticCreatedNotification
    (LocationStatisticCreated DomainEvent) : DomainNotificationEventWrapper<LocationStatisticCreated>(DomainEvent)
{
    public long Id => DomainEvent.LocationStatistic.Id;
    public long LocationId => DomainEvent.LocationStatistic.LocationInformation.Id;
    public string LocationName => DomainEvent.LocationStatistic.LocationInformation.Name;
    public long CameraId => DomainEvent.LocationStatistic.CameraInformation.Id;
    public string CameraName => DomainEvent.LocationStatistic.CameraInformation.Name;
    public long TypeId => DomainEvent.LocationStatistic.TypeInformation.Id;
    public string TypeName => DomainEvent.LocationStatistic.TypeInformation.Name;
    public long MakeId => DomainEvent.LocationStatistic.MakeInformation.Id;
    public string MakeName => DomainEvent.LocationStatistic.MakeInformation.Name;
    public long ModelId => DomainEvent.LocationStatistic.ModelInformation.Id;
    public string ModelName => DomainEvent.LocationStatistic.ModelInformation.Name;
    public long ColorId => DomainEvent.LocationStatistic.ColorInformation.Id;
    public string ColorName => DomainEvent.LocationStatistic.ColorInformation.Name;
    public string Plate => DomainEvent.LocationStatistic.Plate;
    public long TotalPassages => DomainEvent.LocationStatistic.TotalPassages;
    public DateTime FirstSeenAt => DomainEvent.LocationStatistic.FirstSeenAt;
    public DateTime LastSeenAt => DomainEvent.LocationStatistic.LastSeenAt;
}

internal class LocationStatisticCreatedNotificationHandler : IDomainNotificationEventHandler<LocationStatisticCreatedNotification>
{
    private readonly IBus _bus;

    public LocationStatisticCreatedNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(LocationStatisticCreatedNotification notification, CancellationToken cancellationToken)
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
