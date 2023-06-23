using BuildingBlocks.Core.Domain.Exceptions;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Exceptions.Domain;

public class RecordDomainException : DomainException
{
    public RecordDomainException(string message) : base(message)
    {
    }
}
