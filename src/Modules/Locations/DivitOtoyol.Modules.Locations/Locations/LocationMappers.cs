using AutoMapper;
using DivitOtoyol.Modules.Locations.Locations.Dtos;
using DivitOtoyol.Modules.Locations.Locations.Features.CreatingLocation;
using DivitOtoyol.Modules.Locations.Locations.Features.CreatingLocation;
using DivitOtoyol.Modules.Locations.Locations.Models;

namespace DivitOtoyol.Modules.Locations.Locations;

public class LocationMappers : Profile
{
    public LocationMappers()
    {
        CreateMap<Location, LocationDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.ParentId, opt => opt.MapFrom(x => x.ParentId != null ? x.ParentId.Value : 1))
            .ForMember(x => x.Created, opt => opt.MapFrom(x => x.Created));

        CreateMap<CreateLocation, Location>();

        CreateMap<CreateLocationRequest, CreateLocation>()
            .ConstructUsing(req => new CreateLocation(
                req.Name,
                req.ParentId));
    }
}
