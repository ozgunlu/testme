using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Vehicles.Makes.ValueObjects;
using DivitOtoyol.Modules.Vehicles.Models.ValueObjects;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.ChangingModelMake.Events;

public record ModelMakeChangedNotification(MakeId MakeId, ModelId ModelId) : DomainEvent;
