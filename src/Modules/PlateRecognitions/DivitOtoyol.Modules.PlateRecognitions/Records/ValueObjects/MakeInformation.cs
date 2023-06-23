using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.PlateRecognitions.Records.Exceptions.Domain;
using DivitOtoyol.Modules.PlateRecognitions.Makes;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.ValueObjects;

// Here versioning Name is not important for us so we can save it on DB
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
                new RecordDomainException("Make name can't be null.")),
            Id = Guard.Against.Null(id, new RecordDomainException("Make Id can't be  null.")),
        };
    }
}
