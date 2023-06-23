using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Servers.Locations.Exceptions;
using DivitOtoyol.Modules.Servers.Servers.Dtos;
using DivitOtoyol.Modules.Servers.Servers.Models;
using DivitOtoyol.Modules.Servers.Servers.ValueObjects;
using DivitOtoyol.Modules.Servers.Shared.Contracts;
using DivitOtoyol.Modules.Servers.Shared.Location;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Servers.Servers.Features.CreatingServer;

public record CreateServer(
    long LocationId,
    string Name,
    string Ip) : ITxCreateCommand<CreateServerResponse>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

public class CreateServerValidator : AbstractValidator<CreateServer>
{
    public CreateServerValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Id must be greater than 0");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.LocationId)
            .NotEmpty().WithMessage("Location Id is required.");

        RuleFor(x => x.Ip)
            .NotEmpty().WithMessage("Ip is required.");
    }
}

public class CreateServerHandler : ICommandHandler<CreateServer, CreateServerResponse>
{
    private readonly ILocationApiClient _locationApiClient;
    private readonly ILogger<CreateServerHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IServerDbContext _serverDbContext;

    public CreateServerHandler(
        ILocationApiClient locationApiClient,
        IServerDbContext serverDbContext,
        IMapper mapper,
        ILogger<CreateServerHandler> logger)
    {
        _locationApiClient = locationApiClient;
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _serverDbContext = Guard.Against.Null(serverDbContext, nameof(serverDbContext));
    }

    public async Task<CreateServerResponse> Handle(
        CreateServer command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var location = (await _locationApiClient.GetLocationByIdAsync(command.LocationId, cancellationToken))?.Location;
        Guard.Against.NotFound(location, new LocationNotFoundException(command.LocationId));

        var locationInformation = LocationInformation.Create(location!.Id, location.Name);
        Console.WriteLine($"Location Information - Id: {locationInformation.Id}, Name: {locationInformation.Name}");

        var server = Server.Create(
            command.Id,
            locationInformation,
            command.Name,
            command.Ip);

        await _serverDbContext.Servers.AddAsync(server, cancellationToken: cancellationToken);

        await _serverDbContext.SaveChangesAsync(cancellationToken);

        var created = await _serverDbContext.Servers
            .Include(x => x.LocationInformation)
            .SingleOrDefaultAsync(x => x.Id == server.Id, cancellationToken: cancellationToken);

        var serverDto = _mapper.Map<ServerDto>(created);

        _logger.LogInformation("Server a with ID: '{ServerId} created.'", command.Id);

        return new CreateServerResponse(serverDto);
    }
}
