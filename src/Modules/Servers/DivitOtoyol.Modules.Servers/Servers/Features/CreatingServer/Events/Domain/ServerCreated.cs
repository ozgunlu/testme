using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Servers.Servers.Models;
using DivitOtoyol.Modules.Servers.Shared.Data;

namespace DivitOtoyol.Modules.Servers.Servers.Features.CreatingServer.Events.Domain;

public record ServerCreated(Server Server) : DomainEvent;

internal class ServerCreatedHandler : IDomainEventHandler<ServerCreated>
{
    private readonly ServerDbContext _dbContext;

    public ServerCreatedHandler(ServerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(ServerCreated notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
    }
}

// Mapping domain event to integration event in domain event handler is better from mapping in command handler (for preserving our domain rule invariants).
internal class ServerCreatedDomainEventToIntegrationMappingHandler : IDomainEventHandler<ServerCreated>
{
    public ServerCreatedDomainEventToIntegrationMappingHandler()
    {
    }

    public Task Handle(ServerCreated domainEvent, CancellationToken cancellationToken)
    {
        // 1. Mapping DomainEvent To IntegrationEvent
        // 2. Save Integration Event to Outbox
        return Task.CompletedTask;
    }
}
