using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Abstractions.Messaging.Context;
using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.PlateRecognitions.Makes.Features.CreatingMake.Events.External;

public record MakeCreated(long Id, string Name) :
    IntegrationEvent, ITxRequest;

public class MakeCreatedConsumer : IMessageHandler<MakeCreated>
{
    public Task HandleAsync(
        IConsumeContext<MakeCreated> messageContext,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
