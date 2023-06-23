using BuildingBlocks.Core.Domain.Exceptions;

namespace DivitOtoyol.Modules.Vehicles.Types.Exceptions.Domain;

public class TypeDomainException : DomainException
{
    public TypeDomainException(string message) : base(message)
    {
    }
}
