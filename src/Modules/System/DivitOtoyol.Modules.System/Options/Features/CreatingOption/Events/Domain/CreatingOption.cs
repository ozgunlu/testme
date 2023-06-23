using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Systems.Options.ValueObjects;

namespace DivitOtoyol.Modules.Systems.Options.Features.CreatingOption.Events.Domain;

public record CreatingLocation(
    OptionId Id,
    string Key,
    string Value,
    string Modules,
    bool CanUpdate,
    bool CanDelete) : DomainEvent;
