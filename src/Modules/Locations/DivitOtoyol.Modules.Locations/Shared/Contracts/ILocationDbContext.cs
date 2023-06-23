using DivitOtoyol.Modules.Locations.Locations.Models;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Locations.Shared.Contracts;

public interface ILocationDbContext
{
    DbSet<Location> Locations { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
