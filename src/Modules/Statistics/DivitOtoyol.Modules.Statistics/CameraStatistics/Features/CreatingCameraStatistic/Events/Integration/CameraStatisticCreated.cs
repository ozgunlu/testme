using BuildingBlocks.Core.Messaging;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Features.CreatingCameraStatistic.Events.Integration;

public record CameraStatisticCreated(
    long Id,
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
    long TotalPassages,
    DateTime FirstSeenAt,
    DateTime LastSeenAt) :
    IntegrationEvent;
