using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Servers.Servers.Exceptions.Domain;

namespace DivitOtoyol.Modules.Servers.Servers.ValueObjects;

public record Name
{
    public string Value { get; private set; }

    public Name? Null => null;

    public static Name Create(string value)
    {
        return new Name
        {
            Value = Guard.Against.NullOrEmpty(value, new ServerDomainException("Name can't be null mor empty."))
        };
    }

    public static implicit operator Name(string value) => Create(value);

    public static implicit operator string(Name value) =>
        Guard.Against.Null(value.Value, new ServerDomainException("Name can't be null."));
}
