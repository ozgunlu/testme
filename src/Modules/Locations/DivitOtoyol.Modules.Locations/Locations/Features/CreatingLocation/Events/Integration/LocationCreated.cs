using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.Locations.Locations.Features.CreatingLocation.Events.Integration;

public record LocationCreated(long Id, string Name) : IntegrationEvent;
