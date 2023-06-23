using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Servers.Servers.Models;
using DivitOtoyol.Modules.Servers.Shared.Data;

namespace DivitOtoyol.Modules.Servers.Servers.Features.DeletingServer;

public record ServerDeleted(Server Server) : DomainEvent;

internal class ServerDeletedHandler : IDomainEventHandler<ServerDeleted>
{
    private readonly ICommandProcessor _commandProcessor;
    private readonly IMapper _mapper;
    private readonly ServerDbContext _serverDbContext;

    public ServerDeletedHandler(
        ICommandProcessor commandProcessor,
        IMapper mapper,
        ServerDbContext serverDbContext)
    {
        _commandProcessor = commandProcessor;
        _mapper = mapper;
        _serverDbContext = serverDbContext;
    }

    public Task Handle(ServerDeleted notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
