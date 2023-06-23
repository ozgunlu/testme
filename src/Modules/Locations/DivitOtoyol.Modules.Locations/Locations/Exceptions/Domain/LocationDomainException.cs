using BuildingBlocks.Core.Domain.Exceptions;

namespace DivitOtoyol.Modules.Locations.Locations.Exceptions.Domain;

public class LocationDomainException : DomainException
{
    public LocationDomainException(string message) : base(message)
    {
    }
}
