using AutoMapper;
using DivitOtoyol.Modules.Vehicles.Makes.Dtos;
using DivitOtoyol.Modules.Vehicles.Makes.Features.CreatingMake;
using DivitOtoyol.Modules.Vehicles.Makes.Models;

namespace DivitOtoyol.Modules.Vehicles.Makes;

public class MakeMappers : Profile
{
    public MakeMappers()
    {
        CreateMap<Make, MakeDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Created, opt => opt.MapFrom(x => x.Created));

        CreateMap<CreateMake, Make>();

        CreateMap<CreateMakeRequest, CreateMake>()
            .ConstructUsing(req => new CreateMake(
                req.Name));
    }
}
