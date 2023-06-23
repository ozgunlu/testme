using AutoMapper;
using DivitOtoyol.Modules.PlateRecognitions.Records.Dtos;
using DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord;
using DivitOtoyol.Modules.PlateRecognitions.Records.Features.CreatingRecord.Requests;
using DivitOtoyol.Modules.PlateRecognitions.Records.Models.Write;

namespace DivitOtoyol.Modules.PlateRecognitions.Records;

public class RecordMappers : Profile
{
    public RecordMappers()
    {
        CreateMap<Record, RecordDto>()
            .ForMember(x => x.Plate, opt => opt.MapFrom(x => x.Plate.Value))
            .ForMember(x => x.CameraId, opt => opt.MapFrom(x => x.CameraInformation.Id.Value))
            .ForMember(x => x.CameraName, opt => opt.MapFrom(x => x.CameraInformation == null ? "" : x.CameraInformation.Name))
            .ForMember(x => x.MakeId, opt => opt.MapFrom(x => x.MakeInformation.Id.Value))
            .ForMember(x => x.MakeName, opt => opt.MapFrom(x => x.MakeInformation == null ? "" : x.MakeInformation.Name))
            .ForMember(x => x.ModelId, opt => opt.MapFrom(x => x.ModelInformation.Id.Value))
            .ForMember(x => x.ModelName, opt => opt.MapFrom(x => x.ModelInformation == null ? "" : x.ModelInformation.Name))
            .ForMember(x => x.ColorId, opt => opt.MapFrom(x => x.ColorInformation.Id.Value))
            .ForMember(x => x.ColorName, opt => opt.MapFrom(x => x.ColorInformation == null ? "" : x.ColorInformation.Name))
            .ForMember(x => x.LprDate, opt => opt.MapFrom(x => x.LprDate))
            .ForMember(x => x.ImagePath, opt => opt.MapFrom(x => x.ImagePath))
            .ForMember(x => x.Metadata, opt => opt.MapFrom(x => x.Metadata))
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value));

        CreateMap<CreateRecord, Record>();

        CreateMap<CreateRecordRequest, CreateRecord>()
            .ConstructUsing(req => new CreateRecord(
                req.Plate,
                req.CameraId,
                req.OcrMake,
                req.OcrModel,
                req.OcrColor,
                req.LprDate,
                req.Metadata,
                req.ImageData));
    }
}
