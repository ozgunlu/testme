using Ardalis.GuardClauses;
using BuildingBlocks.Core.Exception;
using DivitOtoyol.Modules.PlateRecognitions.Records.Exceptions.Domain;
using DivitOtoyol.Modules.PlateRecognitions.Models;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.ValueObjects;

// Here versioning Name is not important for us so we can save it on DB
public record ModelInformation
{
    public string Name { get; private set; } = null!;
    public ModelId Id { get; private set; } = null!;

    public string TypeName { get; private set; } = null!;

    public static ModelInformation Create(ModelId id, string name, string typeName)
    {
        return new ModelInformation
        {
            Name = Guard.Against.NullOrWhiteSpace(
                name,
                new RecordDomainException("Model name can't be null.")),
            Id = Guard.Against.Null(id, new RecordDomainException("Model Id can't be  null.")),
        };
    }
}
