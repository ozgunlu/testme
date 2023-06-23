using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Types.ValueObjects;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.CreatingType.Events.Domain;

public record CreatingType(
    TypeId Id,
    string Name) : DomainEvent;
