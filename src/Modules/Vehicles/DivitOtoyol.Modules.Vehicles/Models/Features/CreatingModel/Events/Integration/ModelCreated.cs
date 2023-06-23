using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.Vehicles.Models.Features.CreatingModel.Events.Integration;

public record ModelCreated(long Id, string Name) : IntegrationEvent;
