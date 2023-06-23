using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Vehicles.Models.ValueObjects;

public record ModelId : AggregateId<long>
{
    public ModelId(long value) : base(value)
    {
    }

    public static implicit operator long(ModelId id) => Guard.Against.Null(id.Value, nameof(id.Value));

    public static implicit operator ModelId(long id) => new(id);
}
