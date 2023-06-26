using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Statistics.Communications.Types;

public record TypeId : AggregateId<long>
{
    public TypeId(long value) : base(value)
    {
    }

    public static implicit operator long(TypeId id) => Guard.Against.Null(id.Value, nameof(id.Value));

    public static implicit operator TypeId(long id) => new(id);
}
