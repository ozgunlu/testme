using BuildingBlocks.Persistence.EfCore.Postgres;

namespace DivitOtoyol.Modules.Systems.Shared.Data;

public class SystemDbContextDesignFactory : DbContextDesignFactoryBase<SystemDbContext>
{
    public SystemDbContextDesignFactory() : base("Systems:PostgresOptions")
    {
    }
}
