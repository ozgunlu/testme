using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Abstractions.Messaging.Context;
using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.Statistics.Communications.Cameras.Features.CreatingCamera.Events.External;

public record CameraCreated(long Id, string Name) :
    IntegrationEvent, ITxRequest;

public class CameraCreatedConsumer : IMessageHandler<CameraCreated>
{
    public Task HandleAsync(
        IConsumeContext<CameraCreated> messageContext,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
