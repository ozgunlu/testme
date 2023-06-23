using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Locations.Locations.Models;
using DivitOtoyol.Modules.Locations.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Locations.Shared.Data;

public class LocationDbContext : EfDbContextBase, ILocationDbContext
{
    public const string DefaultSchema = "location";

    public LocationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Location>()
            .HasKey(l => l.Id);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Location> Locations => Set<Location>();
}
