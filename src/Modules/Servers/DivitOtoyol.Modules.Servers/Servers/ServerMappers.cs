using AutoMapper;
using DivitOtoyol.Modules.Servers.Servers.Dtos;
using DivitOtoyol.Modules.Servers.Servers.Features.CreatingServer;
using DivitOtoyol.Modules.Servers.Servers.Models;

namespace DivitOtoyol.Modules.Servers.Servers;

public class ServerMappers : Profile
{
    public ServerMappers()
    {
        CreateMap<Server, ServerDto>()
            .ForMember(x => x.LocationName, opt => opt.MapFrom(x => x.LocationInformation.Name))
            .ForMember(x => x.LocationId, opt => opt.MapFrom(x => x.LocationInformation.Id.Value))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name.Value))
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value))
            .ForMember(x => x.Ip, opt => opt.MapFrom(x => x.Ip));

        CreateMap<CreateServer, Server>();

        CreateMap<CreateServerRequest, CreateServer>()
            .ConstructUsing(req => new CreateServer(
                req.LocationId,
                req.Name,
                req.Ip));
    }
}
