using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects;

public record PlateStatisticId : AggregateId
{
    public PlateStatisticId(long value) : base(value)
    {
        Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public static implicit operator long(PlateStatisticId id) => id.Value;

    public static implicit operator PlateStatisticId(long id) => new(id);
}
