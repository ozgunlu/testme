using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Exceptions.Domain;
using DivitOtoyol.Modules.Statistics.Communications.Makes;

namespace DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects;

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
                new CameraStatisticDomainException("Make name can't be null.")),
            Id = Guard.Against.Null(id, new CameraStatisticDomainException("Make Id can't be  null.")),
        };
    }
}
