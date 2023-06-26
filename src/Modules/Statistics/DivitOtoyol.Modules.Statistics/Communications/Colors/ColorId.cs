using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Statistics.Communications.Colors;

public record ColorId : AggregateId<long>
{
    public ColorId(long value) : base(value)
    {
    }

    public static implicit operator long(ColorId id) => Guard.Against.Null(id.Value, nameof(id.Value));

    public static implicit operator ColorId(long id) => new(id);
}
