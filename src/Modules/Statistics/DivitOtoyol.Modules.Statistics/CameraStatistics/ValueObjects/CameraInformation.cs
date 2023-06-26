using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Exceptions.Domain;
using DivitOtoyol.Modules.Statistics.Communications.Cameras;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects;

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
                new CameraStatisticDomainException("Camera name can't be null.")),
            Id = Guard.Against.Null(id, new CameraStatisticDomainException("Camera Id can't be  null.")),
        };
    }
}
