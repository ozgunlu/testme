using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.PlateRecognitions.Cameras;

public record CameraId : AggregateId<long>
{
    public CameraId(long value) : base(value)
    {
    }

    public static implicit operator long(CameraId id) => Guard.Against.Null(id.Value, nameof(id.Value));

    public static implicit operator CameraId(long id) => new(id);
}
