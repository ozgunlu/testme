using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Servers.Locations;

public record LocationId : AggregateId<long>
{
    public LocationId(long value) : base(value)
    {
    }

    public static implicit operator long(LocationId id) => Guard.Against.Null(id.Value, nameof(id.Value));

    public static implicit operator LocationId(long id) => new(id);
}
