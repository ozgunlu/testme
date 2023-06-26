namespace DivitOtoyol.Modules.Statistics.PlateStatistics.Features.CreatingPlateStatistic;

public record CreatePlateStatisticRequest(
    long LocationId,
    string LocationName,
    long CameraId,
    string CameraName,
    long TypeId,
    string TypeName,
    long MakeId,
    string MakeName,
    long ModelId,
    string ModelName,
    long ColorId,
    string ColorName,
    string Plate,
    DateTime LprDate);
