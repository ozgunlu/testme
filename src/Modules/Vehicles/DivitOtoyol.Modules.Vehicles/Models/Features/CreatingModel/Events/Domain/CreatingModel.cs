using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Models.ValueObjects;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.CreatingModel.Events.Domain;

public record CreatingModel(
    ModelId Id,
    string Name) : DomainEvent;
