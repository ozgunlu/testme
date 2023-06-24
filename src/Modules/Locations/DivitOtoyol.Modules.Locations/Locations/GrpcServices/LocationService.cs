using DivitOtoyol.Modules.Locations.Locations.Models;
using DivitOtoyol.Modules.Locations.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Locations.Locations.GrpcServices;

public class LocationService : ILocationService
{
    private readonly ILocationDbContext _dbContext;

    public LocationService(ILocationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Location> GetLocationByIdAsync(long id)
    {
        return await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == id);
    }
}
