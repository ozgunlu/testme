using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Abstractions.Messaging.Context;
using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.Statistics.Communications.Types.Features.CreatingType.Events.External;

public record TypeCreated(long Id, string Name) :
    IntegrationEvent, ITxRequest;

public class TypeCreatedConsumer : IMessageHandler<TypeCreated>
{
    public Task HandleAsync(
        IConsumeContext<TypeCreated> messageContext,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
