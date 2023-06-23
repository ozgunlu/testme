using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Servers.Servers.Features.CreatingServer.Events.Domain;

namespace DivitOtoyol.Modules.Servers.Servers.Features.CreatingServer.Events.Notification;

public record ServerCreatedNotification
    (ServerCreated DomainEvent) : DomainNotificationEventWrapper<ServerCreated>(DomainEvent)
{
    public long Id => DomainEvent.Server.Id;
    public long LocationId => DomainEvent.Server.LocationInformation.Id;
    public string? LocationName => DomainEvent.Server.LocationInformation.Name;
    public string Name => DomainEvent.Server.Name;
}

internal class ServerCreatedNotificationHandler : IDomainNotificationEventHandler<ServerCreatedNotification>
{
    private readonly IBus _bus;

    public ServerCreatedNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(ServerCreatedNotification notification, CancellationToken cancellationToken)
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
