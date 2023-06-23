using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Locations.Locations.ValueObjects;

public record LocationId : AggregateId
{
    public LocationId(long value) : base(value)
    {
        Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public static implicit operator long(LocationId id) => id.Value;

    public static implicit operator LocationId(long id) => new(id);
}
