using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.Vehicles.Makes.Features.CreatingMake.Events.Integration;

public record MakeCreated(long Id, string Name) : IntegrationEvent;
