using BuildingBlocks.Core.Domain.Exceptions;

namespace DivitOtoyol.Modules.Vehicles.Makes.Exceptions.Domain;

public class MakeDomainException : DomainException
{
    public MakeDomainException(string message) : base(message)
    {
    }

    public MakeDomainException(long id) : base($"Category with id: '{id}' not found.")
    {
    }
}
