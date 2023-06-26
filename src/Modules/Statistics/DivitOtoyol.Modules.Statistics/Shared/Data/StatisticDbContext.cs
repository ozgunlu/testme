using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Statistics.CameraStatistics.Models;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Models;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Models;
using DivitOtoyol.Modules.Statistics.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Statistics.Shared.Data;

public class StatisticDbContext : EfDbContextBase, IStatisticDbContext
{
    public const string DefaultSchema = "statistic";

    public StatisticDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<CameraStatistic>()
            .HasKey(l => l.Id);

        modelBuilder.Entity<LocationStatistic>()
            .HasKey(l => l.Id);

        modelBuilder.Entity<PlateStatistic>()
            .HasKey(l => l.Id);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<CameraStatistic> CameraStatistics => Set<CameraStatistic>();
    public DbSet<LocationStatistic> LocationStatistics => Set<LocationStatistic>();
    public DbSet<PlateStatistic> PlateStatistics => Set<PlateStatistic>();
}
