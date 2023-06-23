using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Cameras.Locations.Exceptions;

public class LocationNotFoundException : NotFoundException
{
    public LocationNotFoundException(long id) : base($"Location with id {id} not found")
    {
    }
}
