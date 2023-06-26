using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.Communications.Makes;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Exceptions.Domain;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects;

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
                new LocationStatisticDomainException("Make name can't be null.")),
            Id = Guard.Against.Null(id, new LocationStatisticDomainException("Make Id can't be  null.")),
        };
    }
}
