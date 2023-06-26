using BuildingBlocks.Persistence.EfCore.Postgres;

namespace DivitOtoyol.Modules.Statistics.Shared.Data;

public class StatisticDbContextDesignFactory : DbContextDesignFactoryBase<StatisticDbContext>
{
    public StatisticDbContextDesignFactory() : base("Statistics:PostgresOptions")
    {
    }
}
