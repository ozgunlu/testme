using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Abstractions.Messaging.Context;
using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.PlateRecognitions.Colors.Features.CreatingColor.Events.External;

public record ColorCreated(long Id, string Name) :
    IntegrationEvent, ITxRequest;

public class ColorCreatedConsumer : IMessageHandler<ColorCreated>
{
    public Task HandleAsync(
        IConsumeContext<ColorCreated> messageContext,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
