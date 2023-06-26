using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.Features.CreatingCameraStatistic.Events.Domain;

public record CreatingCameraStatistic(
    CameraStatisticId Id,
    LocationInformation LocationInformation,
    CameraInformation CameraInformation,
    TypeInformation TypeInformation,
    MakeInformation MakeInformation,
    ModelInformation ModelInformation,
    ColorInformation ColorInformation,
    string Plate,
    long TotalPassages,
    DateTime FirstSeenAt,
    DateTime LastSeenAt) : DomainEvent;
