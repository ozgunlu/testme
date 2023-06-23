using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Servers.Servers.Exceptions.Application;
using DivitOtoyol.Modules.Servers.Shared.Data;
using DivitOtoyol.Modules.Servers.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Servers.Servers.Features.DeletingServer;

public record DeleteServer(long Id) : ITxCommand;

internal class DeleteServerValidator : AbstractValidator<DeleteServer>
{
    public DeleteServerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal class DeleteServerHandler : ICommandHandler<DeleteServer>
{
    private readonly ServerDbContext _serverDbContext;
    private readonly ILogger<DeleteServerHandler> _logger;

    public DeleteServerHandler(
        ServerDbContext serverDbContext,
        ILogger<DeleteServerHandler> logger)
    {
        _serverDbContext = serverDbContext;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteServer command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var server = await _serverDbContext.FindServerAsync(command.Id);

        Guard.Against.NotFound(server, new ServerNotFoundException(command.Id));

        _serverDbContext.Servers.Remove(server!);

        await _serverDbContext.SaveChangesAsync(cancellationToken);

        // for raising a deleted domain event
        server!.Delete();

        _logger.LogInformation("Server with id '{Id} removed.'", command.Id);

        return Unit.Value;
    }
}
