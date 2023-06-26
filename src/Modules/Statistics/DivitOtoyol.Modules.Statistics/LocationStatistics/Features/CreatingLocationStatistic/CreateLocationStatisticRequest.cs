namespace DivitOtoyol.Modules.Statistics.LocationStatistics.Features.CreatingLocationStatistic;

public record CreateLocationStatisticRequest(
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
