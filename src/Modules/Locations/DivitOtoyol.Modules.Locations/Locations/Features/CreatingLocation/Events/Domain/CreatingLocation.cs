using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Locations.Locations.ValueObjects;

namespace DivitOtoyol.Modules.Locations.Locations.Features.CreatingLocation.Events.Domain;

public record CreatingLocation(
    LocationId Id,
    string Name) : DomainEvent;
