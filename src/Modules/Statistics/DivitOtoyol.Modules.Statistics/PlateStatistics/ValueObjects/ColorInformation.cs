using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.Communications.Colors;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Exceptions.Domain;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects;

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
                new PlateStatisticDomainException("Color name can't be null.")),
            Id = Guard.Against.Null(id, new PlateStatisticDomainException("Color Id can't be  null.")),
        };
    }
}
