using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Events.Domain;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Events.Notification;

public record RecordCreatedNotification
    (RecordCreated DomainEvent) : DomainNotificationEventWrapper<RecordCreated>(DomainEvent)
{
    public long Id => DomainEvent.Record.Id;
    public string Plate => DomainEvent.Record.Plate;
}

internal class RecordCreatedNotificationHandler : IDomainNotificationEventHandler<RecordCreatedNotification>
{
    private readonly IBus _bus;

    public RecordCreatedNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(RecordCreatedNotification notification, CancellationToken cancellationToken)
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
