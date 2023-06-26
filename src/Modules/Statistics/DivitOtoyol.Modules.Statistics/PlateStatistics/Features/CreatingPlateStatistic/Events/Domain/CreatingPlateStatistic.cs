using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.Features.CreatingPlateStatistic.Events.Domain;

public record CreatingPlateStatistic(
    PlateStatisticId Id,
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
