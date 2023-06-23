using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Vehicles.Makes.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivitOtoyol.Modules.Vehicles.Makes.Data;

public class MakeEntityConfiguration : IEntityTypeConfiguration<Make>
{
    public void Configure(EntityTypeBuilder<Make> builder)
    {
        builder.ToTable("makes", VehicleDbContext.DefaultSchema);
        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, id => id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name).HasColumnType(EfConstants.ColumnTypes.NormalText).IsRequired();
        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
