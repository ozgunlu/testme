using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Makes.Exceptions.Application;
using DivitOtoyol.Modules.Vehicles.Makes.ValueObjects;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.UpdatingMake;

public record UpdateMake(
    long Id,
    string Name) : ITxUpdateCommand;

internal class UpdateMakeValidator : AbstractValidator<UpdateMake>
{
    public UpdateMakeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.Name).NotEmpty();
    }
}

internal class UpdateMakeCommandHandler : ICommandHandler<UpdateMake>
{
    private readonly IVehicleDbContext _vehicleDbContext;

    public UpdateMakeCommandHandler(IVehicleDbContext vehicleDbContext)
    {
        _vehicleDbContext = vehicleDbContext;
    }

    public async Task<Unit> Handle(UpdateMake command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var make = await _vehicleDbContext.FindMakeAsync(command.Id);
        Guard.Against.NotFound(make, new MakeNotFoundException(command.Id));

        make!.ChangeName(command.Name);

        await _vehicleDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
