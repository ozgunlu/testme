using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Locations.Locations.Dtos;
using DivitOtoyol.Modules.Locations.Locations.Exceptions.Application;
using DivitOtoyol.Modules.Locations.Locations.Models;
using DivitOtoyol.Modules.Locations.Shared.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Locations.Locations.Features.CreatingLocation;

public record CreateLocation(string Name, long ParentId)
    : ITxCreateCommand<CreateLocationResponse>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

internal class CreateLocationValidator : AbstractValidator<CreateLocation>
{
    private readonly LocationDbContext _locationDbContext;
    public CreateLocationValidator(LocationDbContext locationDbContext)
    {
        _locationDbContext = locationDbContext;

        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.ParentId)
            .MustAsync(async (parentId, cancellationToken) =>
            {
                // Eğer ParentId null değilse, bu değerin geçerli bir Location kimliği olduğunu doğrula.
                return await _locationDbContext.Locations.AnyAsync(x => x.Id == parentId, cancellationToken);
            })
            .WithMessage("Invalid ParentId specified.");
    }
}

internal class CreateLocationHandler
    : ICommandHandler<CreateLocation, CreateLocationResponse>
{
    private readonly LocationDbContext _locationDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateLocationHandler> _logger;

    public CreateLocationHandler(
        LocationDbContext locationDbContext,
        IMapper mapper,
        ILogger<CreateLocationHandler> logger)
    {
        _locationDbContext = locationDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateLocationResponse> Handle(
        CreateLocation command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        if (_locationDbContext.Locations.Any(x => x.Name == command.Name && x.ParentId == command.ParentId))
            throw new LocationAlreadyExistsException($"Location with name '{command.Name}' and parent id '{command.ParentId}' already exists.");

        var location =
            Location.Create(
                command.Id,
                command.Name,
                command.ParentId);

        await _locationDbContext.AddAsync(location, cancellationToken);

        await _locationDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Location with id '{@Id}' saved successfully", location.Id);

        var locationDto = _mapper.Map<LocationDto>(location);

        return new CreateLocationResponse(locationDto);
    }
}
