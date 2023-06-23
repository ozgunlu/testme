using DivitOtoyol.Modules.Cameras.Cameras.Models;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Cameras.Shared.Contracts;

public interface ICameraDbContext
{
    DbSet<Camera> Cameras { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
