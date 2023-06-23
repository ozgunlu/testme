using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Colors.ValueObjects;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.CreatingColor.Events.Domain;

public record CreatingColor(
    ColorId Id,
    string Name) : DomainEvent;
