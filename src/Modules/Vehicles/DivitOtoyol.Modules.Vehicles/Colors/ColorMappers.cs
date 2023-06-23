using AutoMapper;
using DivitOtoyol.Modules.Vehicles.Colors.Dtos;
using DivitOtoyol.Modules.Vehicles.Colors.Features.CreatingColor;
using DivitOtoyol.Modules.Vehicles.Colors.Models;

namespace DivitOtoyol.Modules.Vehicles.Colors;

public class ColorMappers : Profile
{
    public ColorMappers()
    {
        CreateMap<Color, ColorDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Created, opt => opt.MapFrom(x => x.Created));

        CreateMap<CreateColor, Color>();

        CreateMap<CreateColorRequest, CreateColor>()
            .ConstructUsing(req => new CreateColor(
                req.Name));
    }
}
