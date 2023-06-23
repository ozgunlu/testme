using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Systems.Options.Models;
using DivitOtoyol.Modules.Systems.Shared.Data;

namespace DivitOtoyol.Modules.Systems.Options.Features.UpdatingOption;

public record OptionUpdated(Option Option) : DomainEvent;

public class OptionUpdatedHandler : IDomainEventHandler<OptionUpdated>
{
    private readonly SystemDbContext _dbContext;

    public OptionUpdatedHandler(SystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(OptionUpdated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
