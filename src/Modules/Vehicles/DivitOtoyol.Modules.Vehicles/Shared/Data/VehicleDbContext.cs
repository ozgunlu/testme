using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Vehicles.Colors.Models;
using DivitOtoyol.Modules.Vehicles.Makes.Models;
using DivitOtoyol.Modules.Vehicles.Models.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using VehicleType = DivitOtoyol.Modules.Vehicles.Types.Models.Type;

namespace DivitOtoyol.Modules.Vehicles.Shared.Data;

public class VehicleDbContext : EfDbContextBase, IVehicleDbContext
{
    public const string DefaultSchema = "vehicle";

    public VehicleDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Color> Colors => Set<Color>();

    public DbSet<VehicleType> VehicleTypes => Set<VehicleType>();

    public DbSet<Make> Makes => Set<Make>();

    public DbSet<Model> Models => Set<Model>();
}
