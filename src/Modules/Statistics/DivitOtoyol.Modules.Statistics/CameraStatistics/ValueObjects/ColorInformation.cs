using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Exceptions.Domain;
using DivitOtoyol.Modules.Statistics.Communications.Colors;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects;

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
                new CameraStatisticDomainException("Color name can't be null.")),
            Id = Guard.Against.Null(id, new CameraStatisticDomainException("Color Id can't be  null.")),
        };
    }
}
