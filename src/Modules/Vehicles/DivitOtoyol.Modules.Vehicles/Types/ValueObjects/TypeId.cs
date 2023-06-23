using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Vehicles.Types.ValueObjects;

public record TypeId : AggregateId<long>
{
    public TypeId(long value) : base(value)
    {
    }

    public static implicit operator long(TypeId id) => id.Value;

    public static implicit operator TypeId(long id) => new(id);
}
