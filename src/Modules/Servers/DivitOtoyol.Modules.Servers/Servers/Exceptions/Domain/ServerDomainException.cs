using BuildingBlocks.Core.Domain.Exceptions;

namespace DivitOtoyol.Modules.Servers.Servers.Exceptions.Domain;

public class ServerDomainException : DomainException
{
    public ServerDomainException(string message) : base(message)
    {
    }
}
