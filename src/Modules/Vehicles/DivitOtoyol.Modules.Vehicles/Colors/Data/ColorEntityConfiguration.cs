using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Vehicles.Colors.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivitOtoyol.Modules.Vehicles.Colors.Data;

public class ColorEntityConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.ToTable("colors", VehicleDbContext.DefaultSchema);
        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, id => id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name).HasColumnType(EfConstants.ColumnTypes.NormalText).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
