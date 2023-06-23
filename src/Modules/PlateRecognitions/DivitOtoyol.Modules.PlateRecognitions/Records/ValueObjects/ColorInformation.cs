using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.PlateRecognitions.Records.Exceptions.Domain;
using DivitOtoyol.Modules.PlateRecognitions.Colors;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.ValueObjects;

// Here versioning Name is not important for us so we can save it on DB
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
                new RecordDomainException("Color name can't be null.")),
            Id = Guard.Against.Null(id, new RecordDomainException("Color Id can't be  null.")),
        };
    }
}
