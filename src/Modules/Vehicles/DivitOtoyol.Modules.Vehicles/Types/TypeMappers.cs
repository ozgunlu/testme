using AutoMapper;
using DivitOtoyol.Modules.Vehicles.Types.Dtos;
using DivitOtoyol.Modules.Vehicles.Types.Features.CreatingType;
using VehicleType = DivitOtoyol.Modules.Vehicles.Types.Models.Type;

namespace DivitOtoyol.Modules.Vehicles.Types;

public class TypeMappers : Profile
{
    public TypeMappers()
    {
        CreateMap<VehicleType, TypeDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.ParentId, opt => opt.MapFrom(x => x.ParentId != null ? x.ParentId.Value : 1))
            .ForMember(x => x.Created, opt => opt.MapFrom(x => x.Created));

        CreateMap<CreateType, VehicleType>();

        CreateMap<CreateTypeRequest, CreateType>()
            .ConstructUsing(req => new CreateType(
                req.Name,
                req.ParentId));
    }
}
