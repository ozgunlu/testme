using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.PlateRecognitions.Colors;

public record ColorId : AggregateId<long?>
{
    public ColorId(long? value) : base(value)
    {
    }

    public static implicit operator long?(ColorId id) => id?.Value;

    public static implicit operator ColorId(long? id) => id.HasValue ? new(id) : null;
}
