using DivitOtoyol.Modules.Systems.Options.Models;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Systems.Shared.Contracts;

public interface ISystemDbContext
{
    DbSet<Option> Options { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
