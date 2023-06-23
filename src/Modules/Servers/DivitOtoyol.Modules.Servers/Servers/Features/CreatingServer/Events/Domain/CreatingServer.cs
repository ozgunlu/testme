using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Servers.Servers.ValueObjects;

namespace DivitOtoyol.Modules.Servers.Servers.Features.CreatingServer.Events.Domain;

public record CreatingServer(
    ServerId Id,
    LocationInformation LocationInformation,
    Name Name,
    string? Ip = null) : DomainEvent;
