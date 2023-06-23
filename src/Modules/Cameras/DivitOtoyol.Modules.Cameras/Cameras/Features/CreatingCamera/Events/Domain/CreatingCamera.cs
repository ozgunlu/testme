using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Cameras.Cameras.ValueObjects;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.CreatingCamera.Events.Domain;

public record CreatingCamera(
    CameraId Id,
    string Name) : DomainEvent;
