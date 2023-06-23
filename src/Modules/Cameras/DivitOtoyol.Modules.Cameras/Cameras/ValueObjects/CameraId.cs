using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Domain;

namespace DivitOtoyol.Modules.Cameras.Cameras.ValueObjects;

public record CameraId : AggregateId
{
    public CameraId(long value) : base(value)
    {
        Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public static implicit operator long(CameraId id) => id.Value;

    public static implicit operator CameraId(long id) => new(id);
}
