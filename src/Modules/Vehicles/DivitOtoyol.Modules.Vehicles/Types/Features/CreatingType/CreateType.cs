using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using DivitOtoyol.Modules.Vehicles.Types.Dtos;
using DivitOtoyol.Modules.Vehicles.Types.Exceptions.Application;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VehicleType = DivitOtoyol.Modules.Vehicles.Types.Models.Type;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.CreatingType;

public record CreateType(string Name, long ParentId)
    : ITxCreateCommand<CreateTypeResponse>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

internal class CreateTypeValidator : AbstractValidator<CreateType>
{
    private readonly VehicleDbContext _vehicleDbContext;
    public CreateTypeValidator(VehicleDbContext vehicleDbContext)
    {
        _vehicleDbContext = vehicleDbContext;

        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.ParentId)
            .MustAsync(async (parentId, cancellationToken) =>
            {
                // Bu değerin geçerli bir Type kimliği olduğunu doğrula.
                return await _vehicleDbContext.VehicleTypes.AnyAsync(x => x.Id == parentId, cancellationToken);
            })
            .WithMessage("Invalid ParentId specified.");
    }
}

internal class CreateTypeHandler
    : ICommandHandler<CreateType, CreateTypeResponse>
{
    private readonly VehicleDbContext _vehicleDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateTypeHandler> _logger;

    public CreateTypeHandler(
        VehicleDbContext vehicleDbContext,
        IMapper mapper,
        ILogger<CreateTypeHandler> logger)
    {
        _vehicleDbContext = vehicleDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateTypeResponse> Handle(
        CreateType command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        if (_vehicleDbContext.VehicleTypes.Any(x => x.Name == command.Name && x.ParentId == command.ParentId))
            throw new TypeAlreadyExistsException($"Type with name '{command.Name}' and parent id '{command.ParentId}' already exists.");

        var type =
            VehicleType.Create(
                command.Id,
                command.Name,
                command.ParentId);

        await _vehicleDbContext.AddAsync(type, cancellationToken);

        await _vehicleDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Type with id '{@Id}' saved successfully", type.Id);

        var typeDto = _mapper.Map<TypeDto>(type);

        return new CreateTypeResponse(typeDto);
    }
}
