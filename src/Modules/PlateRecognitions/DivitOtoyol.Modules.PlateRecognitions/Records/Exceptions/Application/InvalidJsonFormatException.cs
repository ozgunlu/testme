using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Exceptions.Application;

public class InvalidJsonFormatException : NotFoundException
{
    public InvalidJsonFormatException(string message) : base(message)
    {
    }

    public InvalidJsonFormatException(string message, Exception innerException) : base(message)
    {
    }
}
