using Ardalis.GuardClauses;
using AutoMapper;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using BuildingBlocks.Core.IdsGenerator;
using DivitOtoyol.Modules.Vehicles.Models.Dtos;
using DivitOtoyol.Modules.Vehicles.Models.Exceptions.Application;
using DivitOtoyol.Modules.Vehicles.Models.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.CreatingModel;

public record CreateModel(string Name, long MakeId, long TypeId)
    : ITxCreateCommand<CreateModelResponse>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

internal class CreateModelValidator : AbstractValidator<CreateModel>
{
    private readonly VehicleDbContext _vehicleDbContext;
    public CreateModelValidator(VehicleDbContext vehicleDbContext)
    {
        _vehicleDbContext = vehicleDbContext;

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.MakeId)
            .NotEmpty()
            .GreaterThan(0).WithMessage("MakeId must be greater than 0");

        RuleFor(x => x.TypeId)
            .NotEmpty()
            .GreaterThan(0).WithMessage("TypeId must be greater than 0");
    }
}

internal class CreateModelHandler
    : ICommandHandler<CreateModel, CreateModelResponse>
{
    private readonly VehicleDbContext _vehicleDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateModelHandler> _logger;

    public CreateModelHandler(
        VehicleDbContext vehicleDbContext,
        IMapper mapper,
        ILogger<CreateModelHandler> logger)
    {
        _vehicleDbContext = vehicleDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateModelResponse> Handle(
        CreateModel command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var existingModel = await _vehicleDbContext.Models
            .Where(m => m.Name.ToLower() == command.Name.ToLower())
            .FirstOrDefaultAsync(cancellationToken);

        if (existingModel != null)
        {
            // If it exists, return the existing one.
            _logger.LogInformation("Model with name '{Name}' already exists", existingModel.Name);
            var existingModelDto = _mapper.Map<ModelDto>(existingModel);
            return new CreateModelResponse(existingModelDto);
        }

        if (_vehicleDbContext.Models.Any(x => x.Name == command.Name && x.MakeId == command.MakeId && x.TypeId == command.TypeId))
            throw new ModelAlreadyExistsException($"Model with name '{command.Name}' and make id '{command.MakeId}' and type id '{command.TypeId}' already exists.");

        var model =
            Model.Create(
                command.Id,
                command.Name,
                command.MakeId,
                command.TypeId);

        await _vehicleDbContext.AddAsync(model, cancellationToken);

        await _vehicleDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Model with id '{@Id}' saved successfully", model.Id);

        var modelDto = _mapper.Map<ModelDto>(model);

        return new CreateModelResponse(modelDto);
    }
}
