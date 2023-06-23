using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.PlateRecognitions.Models;

public record ModelId : AggregateId<long?>
{
    public ModelId(long? value) : base(value)
    {
    }

    public static implicit operator long?(ModelId id) => id?.Value;

    public static implicit operator ModelId(long? id) => id.HasValue ? new(id) : null;
}
