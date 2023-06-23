using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Models.Features.CreatingModel.Events.Domain;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.CreatingModel.Events.Notification;

public record ModelCreatedNotification
    (ModelCreated DomainEvent) : DomainNotificationEventWrapper<ModelCreated>(DomainEvent)
{
    public long Id => DomainEvent.Model.Id;
    public string Name => DomainEvent.Model.Name;
}

internal class ModelCreatedNotificationHandler : IDomainNotificationEventHandler<ModelCreatedNotification>
{
    private readonly IBus _bus;

    public ModelCreatedNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(ModelCreatedNotification notification, CancellationToken cancellationToken)
    {
        return;
    }
}
