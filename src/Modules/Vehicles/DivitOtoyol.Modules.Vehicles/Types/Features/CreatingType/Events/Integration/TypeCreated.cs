using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.Vehicles.Types.Features.CreatingType.Events.Integration;

public record TypeCreated(long Id, string Name) : IntegrationEvent;
