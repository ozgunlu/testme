using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.Communications.Types;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Exceptions.Domain;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects;

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
                new PlateStatisticDomainException("Type name can't be null.")),
            Id = Guard.Against.Null(id, new PlateStatisticDomainException("Type Id can't be  null.")),
        };
    }
}
