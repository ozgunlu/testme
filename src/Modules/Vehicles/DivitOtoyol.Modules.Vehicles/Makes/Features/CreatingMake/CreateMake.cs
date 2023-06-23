using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Vehicles.Makes.Dtos;
using DivitOtoyol.Modules.Vehicles.Makes.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.CreatingMake;

public record CreateMake(string Name)
    : ITxCreateCommand<CreateMakeResponse>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

internal class CreateMakeValidator : AbstractValidator<CreateMake>
{
    private readonly VehicleDbContext _vehicleDbContext;
    public CreateMakeValidator(VehicleDbContext vehicleDbContext)
    {
        _vehicleDbContext = vehicleDbContext;

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}

internal class CreateMakeHandler
    : ICommandHandler<CreateMake, CreateMakeResponse>
{
    private readonly VehicleDbContext _vehicleDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateMakeHandler> _logger;

    public CreateMakeHandler(
        VehicleDbContext vehicleDbContext,
        IMapper mapper,
        ILogger<CreateMakeHandler> logger)
    {
        _vehicleDbContext = vehicleDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateMakeResponse> Handle(
        CreateMake command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var existingMake = await _vehicleDbContext.Makes
            .Where(m => m.Name.ToLower() == command.Name.ToLower())
            .FirstOrDefaultAsync(cancellationToken);

        if (existingMake != null)
        {
            // If it exists, return the existing one.
            _logger.LogInformation("Make with name '{Name}' already exists", existingMake.Name);
            var existingMakeDto = _mapper.Map<MakeDto>(existingMake);
            return new CreateMakeResponse(existingMakeDto);
        }

        var make =
            Make.Create(
                command.Id,
                command.Name);

        await _vehicleDbContext.AddAsync(make, cancellationToken);

        await _vehicleDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Make with id '{@Id}' saved successfully", make.Id);

        var makeDto = _mapper.Map<MakeDto>(make);

        return new CreateMakeResponse(makeDto);
    }
}
