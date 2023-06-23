using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Cameras.Cameras.Exceptions.Domain;
using DivitOtoyol.Modules.Cameras.Locations;

namespace DivitOtoyol.Modules.Cameras.Cameras.ValueObjects;

// Here versioning Name is not important for us so we can save it on DB
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
                new CameraDomainException("Location name can't be null.")),
            Id = Guard.Against.Null(id, new CameraDomainException("Location Id can't be  null.")),
        };
    }
}
