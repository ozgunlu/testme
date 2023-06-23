using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Systems.Options.Models;
using DivitOtoyol.Modules.Systems.Shared.Data;

namespace DivitOtoyol.Modules.Systems.Options.Features.CreatingOption.Events.Domain;

public record OptionCreated(Option Option) : DomainEvent;

internal class OptionCreatedHandler : IDomainEventHandler<OptionCreated>
{
    private readonly SystemDbContext _dbContext;

    public OptionCreatedHandler(SystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(OptionCreated notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
    }
}

internal class OptionCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<OptionCreated>
{
    public OptionCreatedDomainEventToIntegrationMappingHandler()
    {
    }

    public Task Handle(OptionCreated domainEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
