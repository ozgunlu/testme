using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.Communications.Cameras;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Exceptions.Domain;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects;

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
                new PlateStatisticDomainException("Camera name can't be null.")),
            Id = Guard.Against.Null(id, new PlateStatisticDomainException("Camera Id can't be  null.")),
        };
    }
}
