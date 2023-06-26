using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Statistics.Communications.Makes;

public record MakeId : AggregateId<long>
{
    public MakeId(long value) : base(value)
    {
    }

    public static implicit operator long(MakeId id) => Guard.Against.Null(id.Value, nameof(id.Value));

    public static implicit operator MakeId(long id) => new(id);
}
