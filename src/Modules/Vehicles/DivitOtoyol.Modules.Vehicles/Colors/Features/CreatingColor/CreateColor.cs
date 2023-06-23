using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Vehicles.Colors.Dtos;
using DivitOtoyol.Modules.Vehicles.Colors.Exceptions.Application;
using DivitOtoyol.Modules.Vehicles.Colors.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.CreatingColor;

public record CreateColor(string Name)
    : ITxCreateCommand<CreateColorResponse>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

internal class CreateColorValidator : AbstractValidator<CreateColor>
{
    private readonly VehicleDbContext _vehicleDbContext;
    public CreateColorValidator(VehicleDbContext vehicleDbContext)
    {
        _vehicleDbContext = vehicleDbContext;

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}

internal class CreateColorHandler
    : ICommandHandler<CreateColor, CreateColorResponse>
{
    private readonly VehicleDbContext _vehicleDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateColorHandler> _logger;

    public CreateColorHandler(
        VehicleDbContext vehicleDbContext,
        IMapper mapper,
        ILogger<CreateColorHandler> logger)
    {
        _vehicleDbContext = vehicleDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateColorResponse> Handle(
        CreateColor command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var existingColor = await _vehicleDbContext.Colors
            .Where(c => c.Name.ToLower() == command.Name.ToLower())
            .FirstOrDefaultAsync(cancellationToken);

        if (existingColor != null)
        {
            // If it exists, return the existing one.
            _logger.LogInformation("Color with name '{Name}' already exists", existingColor.Name);
            var existingColorDto = _mapper.Map<ColorDto>(existingColor);
            return new CreateColorResponse(existingColorDto);
        }

        var color =
            Color.Create(
                command.Id,
                command.Name);

        await _vehicleDbContext.AddAsync(color, cancellationToken);

        await _vehicleDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Color with id '{@Id}' saved successfully", color.Id);

        var colorDto = _mapper.Map<ColorDto>(color);

        return new CreateColorResponse(colorDto);
    }
}
