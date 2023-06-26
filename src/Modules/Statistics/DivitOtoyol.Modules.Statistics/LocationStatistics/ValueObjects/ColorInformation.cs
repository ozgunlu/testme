using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.Communications.Colors;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Exceptions.Domain;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects;

public record ColorInformation
{
    public string Name { get; private set; } = null!;
    public ColorId Id { get; private set; } = null!;

    public static ColorInformation Create(ColorId id, string name)
    {
        return new ColorInformation
        {
            Name = Guard.Against.NullOrWhiteSpace(
                name,
                new LocationStatisticDomainException("Color name can't be null.")),
            Id = Guard.Against.Null(id, new LocationStatisticDomainException("Color Id can't be  null.")),
        };
    }
}
