using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.Communications.Cameras;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Exceptions.Domain;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects;

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
                new LocationStatisticDomainException("Camera name can't be null.")),
            Id = Guard.Against.Null(id, new LocationStatisticDomainException("Camera Id can't be  null.")),
        };
    }
}
