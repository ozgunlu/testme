using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.Vehicles.Colors.Features.CreatingColor.Events.Integration;

public record ColorCreated(long Id, string Name) : IntegrationEvent;
