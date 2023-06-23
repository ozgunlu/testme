using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Shared.Extensions;
using DivitOtoyol.Modules.Vehicles.Types;
using DivitOtoyol.Modules.Vehicles.Types.ValueObjects;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.ChangingModelType.Events;

public record ChangingModelType(TypeId TypeId) : DomainEvent;

internal class ChangingModelTypeValidationHandler :
    IDomainEventHandler<ChangingModelType>
{
    private readonly IVehicleDbContext _vehicleDbContext;

    public ChangingModelTypeValidationHandler(IVehicleDbContext vehicleDbContext)
    {
        _vehicleDbContext = vehicleDbContext;
    }

    public async Task Handle(ChangingModelType notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification, nameof(notification));
        Guard.Against.NegativeOrZero(notification.TypeId, nameof(notification.TypeId));
        Guard.Against.ExistsType(
            await _vehicleDbContext.TypeExistsAsync(notification.TypeId, cancellationToken: cancellationToken),
            notification.TypeId);
    }
}
