using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.Communications.Models;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Exceptions.Domain;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects;

public record ModelInformation
{
    public string Name { get; private set; } = null!;
    public ModelId Id { get; private set; } = null!;

    public static ModelInformation Create(ModelId id, string name)
    {
        return new ModelInformation
        {
            Name = Guard.Against.NullOrWhiteSpace(
                name,
                new PlateStatisticDomainException("Model name can't be null.")),
            Id = Guard.Against.Null(id, new PlateStatisticDomainException("Model Id can't be  null.")),
        };
    }
}
