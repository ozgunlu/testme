using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Statistics.PlateStatistics.Models;
using DivitOtoyol.Modules.Statistics.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivitOtoyol.Modules.Statistics.LocationStatistics.Data;

public class LocationStatisticEntityTypeConfiguration : IEntityTypeConfiguration<PlateStatistic>
{
    public void Configure(EntityTypeBuilder<PlateStatistic> builder)
    {
        builder.ToTable("location_statistics", StatisticDbContext.DefaultSchema);

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

        builder.OwnsOne(x => x.CameraInformation, p =>
        {
            p.Property(x => x.Id)
                .HasConversion(id => id.Value, id => id);

            p.Property(x => x.Name)
                .HasMaxLength(EfConstants.Lenght.Normal);
        });

        builder.OwnsOne(x => x.TypeInformation, p =>
        {
            p.Property(x => x.Id)
                .HasConversion(id => id.Value, id => id);

            p.Property(x => x.Name)
                .HasMaxLength(EfConstants.Lenght.Normal);
        });

        builder.OwnsOne(x => x.MakeInformation, p =>
        {
            p.Property(x => x.Id)
                .HasConversion(id => id.Value, id => id);

            p.Property(x => x.Name)
                .HasMaxLength(EfConstants.Lenght.Normal);
        });

        builder.OwnsOne(x => x.ModelInformation, p =>
        {
            p.Property(x => x.Id)
                .HasConversion(id => id.Value, id => id);

            p.Property(x => x.Name)
                .HasMaxLength(EfConstants.Lenght.Normal);
        });

        builder.OwnsOne(x => x.ColorInformation, p =>
        {
            p.Property(x => x.Id)
                .HasConversion(id => id.Value, id => id);

            p.Property(x => x.Name)
                .HasMaxLength(EfConstants.Lenght.Normal);
        });

        builder.Property(x => x.Plate)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.TotalPassages)
            .IsRequired();

        builder.Property(x => x.FirstSeenAt)
            .IsRequired();

        builder.Property(x => x.LastSeenAt)
            .IsRequired();
    }
}
