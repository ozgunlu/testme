using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Cameras.Cameras.Models;
using DivitOtoyol.Modules.Cameras.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Cameras.Shared.Data;

public class CameraDbContext : EfDbContextBase, ICameraDbContext
{
    public const string DefaultSchema = "camera";

    public CameraDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Camera>()
            .HasKey(l => l.Id);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Camera> Cameras => Set<Camera>();
}
