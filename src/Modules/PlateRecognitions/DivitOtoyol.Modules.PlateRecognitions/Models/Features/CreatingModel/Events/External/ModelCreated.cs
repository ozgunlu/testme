using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Abstractions.Messaging.Context;
using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.PlateRecognitions.Models.Features.CreatingModel.Events.External;

public record ModelCreated(long Id, string Name) :
    IntegrationEvent, ITxRequest;

public class ModelCreatedConsumer : IMessageHandler<ModelCreated>
{
    public Task HandleAsync(
        IConsumeContext<ModelCreated> messageContext,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
