using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Cameras.Cameras.Models;
using DivitOtoyol.Modules.Cameras.Cameras.ValueObjects;
using DivitOtoyol.Modules.Cameras.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivitOtoyol.Modules.Cameras.Cameras.Data;

public class CameraEntityTypeConfiguration : IEntityTypeConfiguration<Camera>
{
    public void Configure(EntityTypeBuilder<Camera> builder)
    {
        builder.ToTable("cameras", CameraDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, id => id)
            .ValueGeneratedNever();

        builder.OwnsOne(x => x.LocationInformation, p =>
        {
            p.Property(x => x.Id)
                .HasConversion(id => id.Value, id => id);

            p.Property(x => x.Name)
                .HasMaxLength(EfConstants.Lenght.Normal);
        });

        builder.Property(x => x.Name).HasColumnType(EfConstants.ColumnTypes.NormalText).IsRequired();

        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
