using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Servers.Locations.Exceptions;
using DivitOtoyol.Modules.Servers.Servers.Exceptions.Application;
using DivitOtoyol.Modules.Servers.Servers.ValueObjects;
using DivitOtoyol.Modules.Servers.Shared.Contracts;
using DivitOtoyol.Modules.Servers.Shared.Extensions;
using DivitOtoyol.Modules.Servers.Shared.Location;
using FluentValidation;

namespace DivitOtoyol.Modules.Servers.Servers.Features.UpdatingServer;

public record UpdateServer(
    long Id,
    long LocationId,
    string Name,
    string Ip) : ITxUpdateCommand;

internal class UpdateServerValidator : AbstractValidator<UpdateServer>
{
    public UpdateServerValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.LocationId).GreaterThan(0);

        RuleFor(x => x.Name).NotEmpty();
    }
}

internal class UpdateServerCommandHandler : ICommandHandler<UpdateServer>
{
    private readonly IServerDbContext _serverDbContext;
    private readonly ILocationApiClient _locationApiClient;

    public UpdateServerCommandHandler(
        IServerDbContext serverDbContext,
        ILocationApiClient locationApiClient)
    {
        _serverDbContext = serverDbContext;
        _locationApiClient = locationApiClient;
    }

    public async Task<Unit> Handle(UpdateServer command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var server = await _serverDbContext.FindServerByIdAsync(command.Id);
        Guard.Against.NotFound(server, new ServerNotFoundException(command.Id));

        var location = (await _locationApiClient.GetLocationByIdAsync(command.LocationId, cancellationToken))?.Location;
        Guard.Against.NotFound(location, new LocationNotFoundException(command.LocationId));

        var locationInformation = LocationInformation.Create(location!.Id, location.Name);

        server!.ChangeLocationInformation(locationInformation);
        server.ChangeName(command.Name);
        server.ChangeIp(command.Ip);

        await _serverDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
