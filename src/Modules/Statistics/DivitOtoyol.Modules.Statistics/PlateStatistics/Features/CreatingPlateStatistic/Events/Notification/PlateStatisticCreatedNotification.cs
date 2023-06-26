using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Features.CreatingPlateStatistic.Events.Domain;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.Features.CreatingPlateStatistic.Events.Notification;

public record PlateStatisticCreatedNotification
    (PlateStatisticCreated DomainEvent) : DomainNotificationEventWrapper<PlateStatisticCreated>(DomainEvent)
{
    public long Id => DomainEvent.PlateStatistic.Id;
    public long LocationId => DomainEvent.PlateStatistic.LocationInformation.Id;
    public string LocationName => DomainEvent.PlateStatistic.LocationInformation.Name;
    public long CameraId => DomainEvent.PlateStatistic.CameraInformation.Id;
    public string CameraName => DomainEvent.PlateStatistic.CameraInformation.Name;
    public long TypeId => DomainEvent.PlateStatistic.TypeInformation.Id;
    public string TypeName => DomainEvent.PlateStatistic.TypeInformation.Name;
    public long MakeId => DomainEvent.PlateStatistic.MakeInformation.Id;
    public string MakeName => DomainEvent.PlateStatistic.MakeInformation.Name;
    public long ModelId => DomainEvent.PlateStatistic.ModelInformation.Id;
    public string ModelName => DomainEvent.PlateStatistic.ModelInformation.Name;
    public long ColorId => DomainEvent.PlateStatistic.ColorInformation.Id;
    public string ColorName => DomainEvent.PlateStatistic.ColorInformation.Name;
    public string Plate => DomainEvent.PlateStatistic.Plate;
    public long TotalPassages => DomainEvent.PlateStatistic.TotalPassages;
    public DateTime FirstSeenAt => DomainEvent.PlateStatistic.FirstSeenAt;
    public DateTime LastSeenAt => DomainEvent.PlateStatistic.LastSeenAt;
}

internal class PlateStatisticCreatedNotificationHandler : IDomainNotificationEventHandler<PlateStatisticCreatedNotification>
{
    private readonly IBus _bus;

    public PlateStatisticCreatedNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(PlateStatisticCreatedNotification notification, CancellationToken cancellationToken)
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
