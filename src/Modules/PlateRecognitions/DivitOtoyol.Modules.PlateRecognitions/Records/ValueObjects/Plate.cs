using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.PlateRecognitions.Records.Exceptions.Domain;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.ValueObjects;

public record Plate
{
    public string Value { get; private set; }

    public static Plate? Null => null;

    private Plate(string value)
    {
        Value = value;
    }

    public static Plate Create(string value)
    {
        value = RemoveWhiteSpace(value);
        Guard.Against.NullOrEmpty(value, new RecordDomainException("Plate can't be null or empty."));

        return new Plate(value);
    }

    private static string RemoveWhiteSpace(string input)
    {
        return string.IsNullOrEmpty(input) ? input : input.Replace(" ", string.Empty);
    }

    public static implicit operator Plate(string value) => Create(value);

    public static implicit operator string(Plate value) =>
        Guard.Against.Null(value.Value, new RecordDomainException("Plate can't be null."));
}
