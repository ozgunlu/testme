using AutoMapper;
using DivitOtoyol.Modules.Vehicles.Models.Dtos;
using DivitOtoyol.Modules.Vehicles.Models.Features.CreatingModel;
using DivitOtoyol.Modules.Vehicles.Models.Models;

namespace DivitOtoyol.Modules.Vehicles.Models;

public class ModelMappers : Profile
{
    public ModelMappers()
    {
        CreateMap<Model, ModelDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.MakeId, opt => opt.MapFrom(x => x.Make.Id.Value))
            .ForMember(x => x.MakeName, opt => opt.MapFrom(x => x.Make.Name))
            .ForMember(x => x.TypeId, opt => opt.MapFrom(x => x.Type.Id.Value))
            .ForMember(x => x.TypeName, opt => opt.MapFrom(x => x.Type.Name))
            .ForMember(x => x.Created, opt => opt.MapFrom(x => x.Created));

        CreateMap<CreateModel, Model>();

        CreateMap<CreateModelRequest, CreateModel>()
            .ConstructUsing(req => new CreateModel(
                req.Name,
                req.MakeId,
                req.TypeId));
    }
}
