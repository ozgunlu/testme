using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Exceptions.Domain;
using DivitOtoyol.Modules.Statistics.Communications.Types;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects;

public record TypeInformation
{
    public string Name { get; private set; } = null!;
    public TypeId Id { get; private set; } = null!;

    public static TypeInformation Create(TypeId id, string name)
    {
        return new TypeInformation
        {
            Name = Guard.Against.NullOrWhiteSpace(
                name,
                new CameraStatisticDomainException("Type name can't be null.")),
            Id = Guard.Against.Null(id, new CameraStatisticDomainException("Type Id can't be  null.")),
        };
    }
}
