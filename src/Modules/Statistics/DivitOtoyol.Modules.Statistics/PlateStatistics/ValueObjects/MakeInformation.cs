using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.Communications.Makes;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Exceptions.Domain;

namespace DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects;

public record MakeInformation
{
    public string Name { get; private set; } = null!;
    public MakeId Id { get; private set; } = null!;

    public static MakeInformation Create(MakeId id, string name)
    {
        return new MakeInformation
        {
            Name = Guard.Against.NullOrWhiteSpace(
                name,
                new PlateStatisticDomainException("Make name can't be null.")),
            Id = Guard.Against.Null(id, new PlateStatisticDomainException("Make Id can't be  null.")),
        };
    }
}
