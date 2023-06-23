using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.PlateRecognitions.Records.Exceptions.Domain;
using DivitOtoyol.Modules.PlateRecognitions.Cameras;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.ValueObjects;

// Here versioning Name is not important for us so we can save it on DB
public record CameraInformation
{
    public string Name { get; private set; } = null!;
    public CameraId Id { get; private set; } = null!;

    public static CameraInformation Create(CameraId id, string name)
    {
        return new CameraInformation
        {
            Name = Guard.Against.NullOrWhiteSpace(
                name,
                new RecordDomainException("Camera name can't be null.")),
            Id = Guard.Against.Null(id, new RecordDomainException("Camera Id can't be  null.")),
        };
    }
}
