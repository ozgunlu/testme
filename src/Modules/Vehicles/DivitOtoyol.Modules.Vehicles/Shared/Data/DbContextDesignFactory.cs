using BuildingBlocks.Persistence.EfCore.Postgres;

namespace DivitOtoyol.Modules.Vehicles.Shared.Data;

public class VehicleDbContextDesignFactory : DbContextDesignFactoryBase<VehicleDbContext>
{
    public VehicleDbContextDesignFactory() : base("Vehicles:PostgresOptions")
    {
    }
}
