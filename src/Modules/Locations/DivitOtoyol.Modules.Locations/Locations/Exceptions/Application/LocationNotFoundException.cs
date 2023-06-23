using BuildingBlocks.Core.Exception.Types;

namespace DivitOtoyol.Modules.Locations.Locations.Exceptions.Application;

public class LocationNotFoundException : NotFoundException
{
    public LocationNotFoundException(long id) : base($"Kayıt bulunamadı: '{id}'")
    {
    }

    public LocationNotFoundException(string message) : base(message)
    {
    }
}
