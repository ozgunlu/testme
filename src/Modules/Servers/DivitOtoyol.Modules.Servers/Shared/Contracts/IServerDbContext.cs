using DivitOtoyol.Modules.Servers.Servers.Models;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Servers.Shared.Contracts;

public interface IServerDbContext
{
    DbSet<Server> Servers { get; }

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
