using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects;

public record LocationStatisticId : AggregateId
{
    public LocationStatisticId(long value) : base(value)
    {
        Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public static implicit operator long(LocationStatisticId id) => id.Value;

    public static implicit operator LocationStatisticId(long id) => new(id);
}
