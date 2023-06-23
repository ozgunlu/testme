using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Servers.Servers.Models;
using DivitOtoyol.Modules.Servers.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Servers.Shared.Data;

public class ServerDbContext : EfDbContextBase, IServerDbContext
{
    public const string DefaultSchema = "server";

    public ServerDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Server>()
            .HasKey(l => l.Id);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Server> Servers => Set<Server>();
}
