using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Locations.Locations.Exceptions.Application;

internal class LocationAlreadyExistsException : AppException
{
    public LocationAlreadyExistsException(string message) : base(message)
    {
    }
}
