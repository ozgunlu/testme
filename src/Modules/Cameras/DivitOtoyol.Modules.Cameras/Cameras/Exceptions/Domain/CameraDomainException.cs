using BuildingBlocks.Core.Domain.Exceptions;

namespace DivitOtoyol.Modules.Cameras.Cameras.Exceptions.Domain;

public class CameraDomainException : DomainException
{
    public CameraDomainException(string message) : base(message)
    {
    }
}
