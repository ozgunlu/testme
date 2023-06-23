using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Servers.Servers.ValueObjects;

public record ServerId : AggregateId
{
    public ServerId(long value) : base(value)
    {
        Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public static implicit operator long(ServerId id) => id.Value;

    public static implicit operator ServerId(long id) => new(id);
}
