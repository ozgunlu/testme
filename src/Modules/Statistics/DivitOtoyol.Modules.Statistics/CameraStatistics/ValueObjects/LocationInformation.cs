using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Exceptions.Domain;
using DivitOtoyol.Modules.Statistics.Communications.Locations;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects;

public record LocationInformation
{
    public string Name { get; private set; } = null!;
    public LocationId Id { get; private set; } = null!;

    public static LocationInformation Create(LocationId id, string name)
    {
        return new LocationInformation
        {
            Name = Guard.Against.NullOrWhiteSpace(
                name,
                new CameraStatisticDomainException("Location name can't be null.")),
            Id = Guard.Against.Null(id, new CameraStatisticDomainException("Location Id can't be  null.")),
        };
    }
}
