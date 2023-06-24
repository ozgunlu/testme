using DivitOtoyol.Modules.Locations.Locations.Models;

namespace DivitOtoyol.Modules.Locations.Locations.GrpcServices;

public interface ILocationService
{
    Task<Location> GetLocationByIdAsync(long id);
}
