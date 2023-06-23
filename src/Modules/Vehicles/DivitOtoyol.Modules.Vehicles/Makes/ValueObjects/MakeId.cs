using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Vehicles.Makes.ValueObjects;

public record MakeId : AggregateId
{
    public MakeId(long value) : base(value)
    {
    }

    public static implicit operator long(MakeId id) => id.Value;

    public static implicit operator MakeId(long id) => new(id);
}
