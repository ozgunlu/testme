using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects;

public record CameraStatisticId : AggregateId
{
    public CameraStatisticId(long value) : base(value)
    {
        Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public static implicit operator long(CameraStatisticId id) => id.Value;

    public static implicit operator CameraStatisticId(long id) => new(id);
}
