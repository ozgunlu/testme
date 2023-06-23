using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Models.Exceptions.Application;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.DeletingModel;

public record DeleteModel(long Id) : ITxCommand;

internal class DeleteModelValidator : AbstractValidator<DeleteModel>
{
    public DeleteModelValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

internal class DeleteModelHandler : ICommandHandler<DeleteModel>
{
    private readonly VehicleDbContext _vehicleDbContext;
    private readonly ILogger<DeleteModelHandler> _logger;

    public DeleteModelHandler(
        VehicleDbContext vehicleDbContext,
        ILogger<DeleteModelHandler> logger)
    {
        _vehicleDbContext = vehicleDbContext;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteModel command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var model = await _vehicleDbContext.FindModelAsync(command.Id);

        Guard.Against.NotFound(model, new ModelNotFoundException(command.Id));

        _vehicleDbContext.Models.Remove(model!);

        await _vehicleDbContext.SaveChangesAsync(cancellationToken);

        // for raising a deleted domain event
        model!.Delete();

        _logger.LogInformation("Model with id '{Id} removed.'", command.Id);

        return Unit.Value;
    }
}
