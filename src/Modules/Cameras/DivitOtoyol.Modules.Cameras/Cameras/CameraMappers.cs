using AutoMapper;
using DivitOtoyol.Modules.Cameras.Cameras.Dtos;
using DivitOtoyol.Modules.Cameras.Cameras.Features.CreatingCamera;
using DivitOtoyol.Modules.Cameras.Cameras.Models;

namespace DivitOtoyol.Modules.Cameras.Cameras;

public class CameraMappers : Profile
{
    public CameraMappers()
    {
        CreateMap<Camera, CameraDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value))
            .ForMember(x => x.LocationId, opt => opt.MapFrom(x => x.LocationInformation.Id.Value))
            .ForMember(x => x.LocationName, opt => opt.MapFrom(x => x.LocationInformation.Name))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.BiosName, opt => opt.MapFrom(x => x.BiosName))
            .ForMember(x => x.SerialNumber, opt => opt.MapFrom(x => x.SerialNumber))
            .ForMember(x => x.Ip, opt => opt.MapFrom(x => x.Ip))
            .ForMember(x => x.Created, opt => opt.MapFrom(x => x.Created));

        CreateMap<CreateCamera, Camera>();

        CreateMap<CreateCameraRequest, CreateCamera>()
            .ConstructUsing(req => new CreateCamera(
                req.LocationId,
                req.Name,
                req.BiosName,
                req.SerialNumber,
                req.Ip));
    }
}
