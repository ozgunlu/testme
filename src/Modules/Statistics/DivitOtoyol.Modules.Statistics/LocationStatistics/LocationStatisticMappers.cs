using AutoMapper;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Dtos;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Features.CreatingLocationStatistic;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Models;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics;

public class LocationStatisticMappers : Profile
{
    public LocationStatisticMappers()
    {
        CreateMap<LocationStatistic, LocationStatisticDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value))
            .ForMember(x => x.LocationName, opt => opt.MapFrom(x => x.LocationInformation.Name))
            .ForMember(x => x.LocationId, opt => opt.MapFrom(x => x.LocationInformation.Id.Value))
            .ForMember(x => x.CameraName, opt => opt.MapFrom(x => x.CameraInformation.Name))
            .ForMember(x => x.CameraId, opt => opt.MapFrom(x => x.CameraInformation.Id.Value))
            .ForMember(x => x.TypeName, opt => opt.MapFrom(x => x.TypeInformation.Name))
            .ForMember(x => x.TypeId, opt => opt.MapFrom(x => x.TypeInformation.Id.Value))
            .ForMember(x => x.MakeName, opt => opt.MapFrom(x => x.MakeInformation.Name))
            .ForMember(x => x.MakeId, opt => opt.MapFrom(x => x.MakeInformation.Id.Value))
            .ForMember(x => x.ModelName, opt => opt.MapFrom(x => x.ModelInformation.Name))
            .ForMember(x => x.ModelId, opt => opt.MapFrom(x => x.ModelInformation.Id.Value))
            .ForMember(x => x.ColorName, opt => opt.MapFrom(x => x.ColorInformation.Name))
            .ForMember(x => x.ColorId, opt => opt.MapFrom(x => x.ColorInformation.Id.Value))
            .ForMember(x => x.Plate, opt => opt.MapFrom(x => x.Plate))
            .ForMember(x => x.TotalPassages, opt => opt.MapFrom(x => x.TotalPassages))
            .ForMember(x => x.FirstSeenAt, opt => opt.MapFrom(x => x.FirstSeenAt))
            .ForMember(x => x.LastSeenAt, opt => opt.MapFrom(x => x.LastSeenAt))
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value));

        CreateMap<CreateLocationStatistic, LocationStatistic>();

        CreateMap<CreateLocationStatisticRequest, CreateLocationStatistic>()
            .ConstructUsing(req => new CreateLocationStatistic(
                req.LocationId,
                req.LocationName,
                req.CameraId,
                req.CameraName,
                req.TypeId,
                req.TypeName,
                req.MakeId,
                req.MakeName,
                req.ModelId,
                req.ModelName,
                req.ColorId,
                req.ColorName,
                req.Plate,
                req.LprDate));
    }
}
