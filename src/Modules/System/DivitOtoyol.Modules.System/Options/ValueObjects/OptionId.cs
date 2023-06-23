using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Systems.Options.ValueObjects;

public record OptionId : AggregateId
{
    public OptionId(long value) : base(value)
    {
        Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public static implicit operator long(OptionId id) => id.Value;

    public static implicit operator OptionId(long id) => new(id);
}
