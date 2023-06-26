using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.Communications.Models;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Exceptions.Domain;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects;

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
                new LocationStatisticDomainException("Model name can't be null.")),
            Id = Guard.Against.Null(id, new LocationStatisticDomainException("Model Id can't be  null.")),
        };
    }
}
