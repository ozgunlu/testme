using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.Servers.Servers.Features.CreatingServer.Events.Integration;

public record ServerCreated(long Id, long LocationId, string LocationName,  string Name, string Ip) :
    IntegrationEvent;
