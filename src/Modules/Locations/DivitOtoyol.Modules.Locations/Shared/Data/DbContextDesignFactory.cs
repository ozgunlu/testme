using BuildingBlocks.Persistence.EfCore.Postgres;

namespace DivitOtoyol.Modules.Locations.Shared.Data;

public class LocationDbContextDesignFactory : DbContextDesignFactoryBase<LocationDbContext>
{
    public LocationDbContextDesignFactory() : base("Locations:PostgresOptions")
    {
    }
}
