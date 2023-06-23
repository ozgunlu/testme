using BuildingBlocks.Core.CQRS.Query;
using DivitOtoyol.Modules.Servers.Servers.Dtos;

namespace DivitOtoyol.Modules.Servers.Servers.Features.GettingServers;

public record GetServersResponse(ListResultModel<ServerDto> Servers);
