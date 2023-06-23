using BuildingBlocks.Persistence.EfCore.Postgres;

namespace DivitOtoyol.Modules.Cameras.Shared.Data;

public class CameraDbContextDesignFactory : DbContextDesignFactoryBase<CameraDbContext>
{
    public CameraDbContextDesignFactory() : base("Cameras:PostgresOptions")
    {
    }
}
