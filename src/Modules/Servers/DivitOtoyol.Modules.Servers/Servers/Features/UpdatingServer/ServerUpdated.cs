using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Servers.Servers.Models;
using DivitOtoyol.Modules.Servers.Shared.Data;

namespace DivitOtoyol.Modules.Servers.Servers.Features.UpdatingServer;

public record ServerUpdated(Server Server) : DomainEvent;

public class ServerUpdatedHandler : IDomainEventHandler<ServerUpdated>
{
    private readonly ServerDbContext _dbContext;

    public ServerUpdatedHandler(ServerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(ServerUpdated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
