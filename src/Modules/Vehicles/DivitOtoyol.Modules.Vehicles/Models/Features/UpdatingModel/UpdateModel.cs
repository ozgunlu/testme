using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Makes.Exceptions.Application;
using DivitOtoyol.Modules.Vehicles.Models.Exceptions.Application;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;
using DivitOtoyol.Modules.Vehicles.Types.Exceptions.Application;
using FluentValidation;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.UpdatingModel;

public record UpdateModel(
    long Id,
    string Name,
    long MakeId,
    long TypeId) : ITxUpdateCommand;

internal class UpdateModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateModelValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.MakeId).GreaterThan(0);

        RuleFor(x => x.TypeId).GreaterThan(0);
    }
}

internal class UpdateModelCommandHandler : ICommandHandler<UpdateModel>
{
    private readonly IVehicleDbContext _vehicleDbContext;

    public UpdateModelCommandHandler(IVehicleDbContext vehicleDbContext)
    {
        _vehicleDbContext = vehicleDbContext;
    }

    public async Task<Unit> Handle(UpdateModel command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var model = await _vehicleDbContext.FindModelAsync(command.Id);
        Guard.Against.NotFound(model, new ModelNotFoundException(command.Id));

        model!.ChangeName(command.Name);

        var make = await _vehicleDbContext.FindMakeAsync(command.MakeId);
        Guard.Against.NotFound(make, new MakeNotFoundException(command.MakeId));

        model.ChangeMake(command.MakeId);

        var type = await _vehicleDbContext.FindTypeAsync(command.TypeId);
        Guard.Against.NotFound(type, new TypeNotFoundException(command.TypeId));

        model.ChangeType(command.TypeId);

        await _vehicleDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
