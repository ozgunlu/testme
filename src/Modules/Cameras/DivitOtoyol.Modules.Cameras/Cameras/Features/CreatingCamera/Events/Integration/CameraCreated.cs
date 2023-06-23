using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.Cameras.Cameras.Features.CreatingCamera.Events.Integration;

public record CameraCreated(long Id, string Name) : IntegrationEvent;
