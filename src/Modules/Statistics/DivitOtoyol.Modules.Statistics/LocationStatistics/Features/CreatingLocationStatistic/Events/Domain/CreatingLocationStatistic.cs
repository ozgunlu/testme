using BuildingBlocks.Core.CQRS.Event.Internal;
using DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.Features.CreatingLocationStatistic.Events.Domain;

public record CreatingLocationStatistic(
    LocationStatisticId Id,
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
