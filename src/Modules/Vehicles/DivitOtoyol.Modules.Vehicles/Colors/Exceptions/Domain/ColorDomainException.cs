using BuildingBlocks.Core.Domain.Exceptions;

namespace DivitOtoyol.Modules.Vehicles.Colors.Exceptions.Domain;

public class ColorDomainException : DomainException
{
    public ColorDomainException(string message) : base(message)
    {
    }

    public ColorDomainException(long id) : base($"Category with id: '{id}' not found.")
    {
    }
}
