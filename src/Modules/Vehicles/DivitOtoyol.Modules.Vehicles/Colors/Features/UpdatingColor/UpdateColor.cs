using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Vehicles.Colors.Exceptions.Application;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;
using FluentValidation;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.UpdatingColor;

public record UpdateColor(
    long Id,
    string Name) : ITxUpdateCommand;

internal class UpdateColorValidator : AbstractValidator<UpdateColor>
{
    public UpdateColorValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.Name).NotEmpty();
    }
}

internal class UpdateColorCommandHandler : ICommandHandler<UpdateColor>
{
    private readonly IVehicleDbContext _vehicleDbContext;

    public UpdateColorCommandHandler(IVehicleDbContext vehicleDbContext)
    {
        _vehicleDbContext = vehicleDbContext;
    }

    public async Task<Unit> Handle(UpdateColor command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var color = await _vehicleDbContext.FindColorAsync(command.Id);
        Guard.Against.NotFound(color, new ColorNotFoundException(command.Id));

        color!.ChangeName(command.Name);

        await _vehicleDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
