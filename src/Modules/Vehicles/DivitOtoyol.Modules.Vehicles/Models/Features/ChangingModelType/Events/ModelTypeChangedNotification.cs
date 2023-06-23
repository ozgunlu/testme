using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Models.ValueObjects;
using DivitOtoyol.Modules.Vehicles.Types.ValueObjects;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.ChangingModelType.Events;

public record ModelTypeChangedNotification(TypeId TypeId, ModelId ModelId) : DomainEvent;
