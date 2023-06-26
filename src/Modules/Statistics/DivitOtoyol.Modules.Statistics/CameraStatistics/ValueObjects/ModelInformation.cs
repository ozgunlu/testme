using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Exceptions.Domain;
using DivitOtoyol.Modules.Statistics.Communications.Models;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects;

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
                new CameraStatisticDomainException("Model name can't be null.")),
            Id = Guard.Against.Null(id, new CameraStatisticDomainException("Model Id can't be  null.")),
        };
    }
}
