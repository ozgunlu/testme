using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Systems.Options.Models;
using DivitOtoyol.Modules.Systems.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Systems.Shared.Data;

public class SystemDbContext : EfDbContextBase, ISystemDbContext
{
    public const string DefaultSchema = "system";

    public SystemDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Option>()
            .HasKey(l => l.Id);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Option> Options => Set<Option>();
}
