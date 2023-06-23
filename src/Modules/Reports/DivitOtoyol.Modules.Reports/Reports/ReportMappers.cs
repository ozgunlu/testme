using AutoMapper;
using DivitOtoyol.Modules.Systems.Options.Dtos;
using DivitOtoyol.Modules.Systems.Options.Features.CreatingOption;
using DivitOtoyol.Modules.Systems.Options.Models;

namespace DivitOtoyol.Modules.Reports.Reports;

public class OptionMappers : Profile
{
    public OptionMappers()
    {
        CreateMap<Option, OptionDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value))
            .ForMember(x => x.Key, opt => opt.MapFrom(x => x.Key))
            .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Value))
            .ForMember(x => x.Modules, opt => opt.MapFrom(x => x.Modules))
            .ForMember(x => x.CanUpdate, opt => opt.MapFrom(x => x.CanUpdate))
            .ForMember(x => x.CanDelete, opt => opt.MapFrom(x => x.CanDelete))
            .ForMember(x => x.Created, opt => opt.MapFrom(x => x.Created));

        CreateMap<CreateOption, Option>();

        CreateMap<CreateOptionRequest, CreateOption>()
            .ConstructUsing(req => new CreateOption(
                req.Key,
                req.Value,
                req.Modules,
                req.CanUpdate,
                req.CanDelete));
    }
}
