using BuildingBlocks.Persistence.EfCore.Postgres;
namespace DivitOtoyol.Modules.Servers.Shared.Data;

public class ServerDbContextDesignFactory : DbContextDesignFactoryBase<ServerDbContext>
{
    public ServerDbContextDesignFactory() : base("Servers:PostgresOptions")
    {
    }
}
