using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Systems.Options.Features.CreatingOption.Events.Domain;

namespace DivitOtoyol.Modules.Systems.Options.Features.CreatingOption.Events.Notification;

public record OptionCreatedNotification
    (OptionCreated DomainEvent) : DomainNotificationEventWrapper<OptionCreated>(DomainEvent)
{
    public long Id => DomainEvent.Option.Id;
    public string Key => DomainEvent.Option.Key;
    public string Value => DomainEvent.Option.Value;
    public string Modules => DomainEvent.Option.Modules;
    public bool CanUpdate => DomainEvent.Option.CanUpdate;
    public bool CanDelete => DomainEvent.Option.CanDelete;
}

internal class OptionCreatedNotificationHandler : IDomainNotificationEventHandler<OptionCreatedNotification>
{
    private readonly IBus _bus;

    public OptionCreatedNotificationHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OptionCreatedNotification notification, CancellationToken cancellationToken)
    {
        return;
    }
}
