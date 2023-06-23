using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;
using DivitOtoyol.Modules.Vehicles.Types.Exceptions.Application;
using FluentValidation;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.UpdatingType;

public record UpdateType(
    long Id,
    string Name,
    long ParentId) : ITxUpdateCommand;

internal class UpdateTypeValidator : AbstractValidator<UpdateType>
{
    public UpdateTypeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.Name).NotEmpty();
    }
}

internal class UpdateTypeCommandHandler : ICommandHandler<UpdateType>
{
    private readonly IVehicleDbContext _vehicleDbContext;

    public UpdateTypeCommandHandler(IVehicleDbContext vehicleDbContext)
    {
        _vehicleDbContext = vehicleDbContext;
    }

    public async Task<Unit> Handle(UpdateType command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var type = await _vehicleDbContext.FindTypeAsync(command.Id);
        Guard.Against.NotFound(type, new TypeNotFoundException(command.Id));

        type!.ChangeName(command.Name);

        var parentType = await _vehicleDbContext.FindTypeAsync(command.ParentId);
        Guard.Against.NotFound(parentType, new TypeNotFoundException(command.ParentId));

        type.SetParent(command.ParentId);

        await _vehicleDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
