using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Makes.ValueObjects;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.CreatingMake.Events.Domain;

public record CreatingMake(
    MakeId Id,
    string Name) : DomainEvent;
