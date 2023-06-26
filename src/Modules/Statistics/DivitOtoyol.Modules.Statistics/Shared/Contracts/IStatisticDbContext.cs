using DivitOtoyol.Modules.Statistics.CameraStatistics.Models;
using DivitOtoyol.Modules.Statistics.LocationStatistics.Models;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Models;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Statistics.Shared.Contracts;

public interface IStatisticDbContext
{
    DbSet<CameraStatistic> CameraStatistics { get; }
    DbSet<LocationStatistic> LocationStatistics { get; }
    DbSet<PlateStatistic> PlateStatistics { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
