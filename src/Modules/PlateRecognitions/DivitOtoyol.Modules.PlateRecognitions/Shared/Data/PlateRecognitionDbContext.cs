using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.PlateRecognitions.Records.Models.Write;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Data;

public class PlateRecognitionDbContext : EfDbContextBase, IPlateRecognitionDbContext
{
    public const string DefaultSchema = "plate_recognition";

    public PlateRecognitionDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Record> Records => Set<Record>();
}
