using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.PlateRecognitions.Makes;

public record MakeId : AggregateId<long?>
{
    public MakeId(long? value) : base(value)
    {
    }

    public static implicit operator long?(MakeId id) => id?.Value;

    public static implicit operator MakeId(long? id) => id.HasValue ? new(id) : null;
}
