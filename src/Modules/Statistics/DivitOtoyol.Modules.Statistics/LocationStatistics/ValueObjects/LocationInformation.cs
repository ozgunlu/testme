using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.Communications.Locations;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Exceptions.Domain;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects;

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
                new LocationStatisticDomainException("Location name can't be null.")),
            Id = Guard.Against.Null(id, new LocationStatisticDomainException("Location Id can't be  null.")),
        };
    }
}
