using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Makes;
using DivitOtoyol.Modules.Vehicles.Makes.ValueObjects;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.ChangingModelMake.Events;

public record ChangingModelMake(MakeId MakeId) : DomainEvent;

internal class ChangingModelMakeValidationHandler :
    IDomainEventHandler<ChangingModelMake>
{
    private readonly IVehicleDbContext _vehicleDbContext;

    public ChangingModelMakeValidationHandler(IVehicleDbContext vehicleDbContext)
    {
        _vehicleDbContext = vehicleDbContext;
    }

    public async Task Handle(ChangingModelMake notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
        Guard.Against.NegativeOrZero(notification.MakeId, nameof(notification.MakeId));
        Guard.Against.ExistsMake(
            await _vehicleDbContext.MakeExistsAsync(notification.MakeId, cancellationToken: cancellationToken),
            notification.MakeId);
    }
}
