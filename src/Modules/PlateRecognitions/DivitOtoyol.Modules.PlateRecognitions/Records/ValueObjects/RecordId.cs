using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.ValueObjects;

public record RecordId : AggregateId
{
    public RecordId(long value) : base(value)
    {
        Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public static implicit operator long(RecordId id) => id.Value;

    public static implicit operator RecordId(long id) => new(id);
}
