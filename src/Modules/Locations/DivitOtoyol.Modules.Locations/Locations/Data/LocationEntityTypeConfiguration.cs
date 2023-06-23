using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Locations.Locations.Models;
using DivitOtoyol.Modules.Locations.Locations.ValueObjects;
using DivitOtoyol.Modules.Locations.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivitOtoyol.Modules.Locations.Locations.Data;

public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations", LocationDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, id => id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name).HasColumnType(EfConstants.ColumnTypes.NormalText).IsRequired();

        builder.Property(x => x.ParentId).IsRequired();
        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
