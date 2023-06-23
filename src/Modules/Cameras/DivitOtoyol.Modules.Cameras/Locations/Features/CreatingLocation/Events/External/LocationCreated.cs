using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Abstractions.Messaging.Context;
using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.Cameras.Locations.Features.CreatingLocation.Events.External;

public record LocationCreated(long Id, string Name) :
    IntegrationEvent, ITxRequest;

public class LocationCreatedConsumer : IMessageHandler<LocationCreated>
{
    public Task HandleAsync(
        IConsumeContext<LocationCreated> messageContext,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
