using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.PlateRecognitions.Records.Models.Write;
using DivitOtoyol.Modules.PlateRecognitions.Records.ValueObjects;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json.Linq;

namespace DivitOtoyol.Modules.PlateRecognitions.Records.Data;

public class RecordEntityTypeConfiguration : IEntityTypeConfiguration<Record>
{
    public void Configure(EntityTypeBuilder<Record> builder)
    {
        builder.ToTable("records", PlateRecognitionDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, id => id)
            .ValueGeneratedNever();

        builder.Property(x => x.Plate)
            .HasColumnType(EfConstants.ColumnTypes.NormalText)
            .HasConversion(plate => plate.Value, plate => Plate.Create(plate))
            .IsRequired();

        builder.OwnsOne(x => x.CameraInformation, p =>
        {
            p.Property(x => x.Id)
                .HasConversion(id => id.Value, id => id);

            p.Property(x => x.Name)
                .HasMaxLength(EfConstants.Lenght.Normal);
        });

        builder.OwnsOne(x => x.MakeInformation, p =>
        {
            p.Property(x => x.Id)
                .HasConversion(id => id.Value, id => id)
                .IsRequired(false);

            p.Property(x => x.Name)
                .HasMaxLength(EfConstants.Lenght.Normal)
                .IsRequired(false);
        });

        builder.OwnsOne(x => x.ModelInformation, p =>
        {
            p.Property(x => x.Id)
                .HasConversion(id => id.Value, id => id)
                .IsRequired(false);

            p.Property(x => x.Name)
                .HasMaxLength(EfConstants.Lenght.Normal)
                .IsRequired(false);
        });

        builder.OwnsOne(x => x.ColorInformation, p =>
        {
            p.Property(x => x.Id)
                .HasConversion(id => id.Value, id => id)
                .IsRequired(false);

            p.Property(x => x.Name)
                .HasMaxLength(EfConstants.Lenght.Normal)
                .IsRequired(false);
        });

        builder.Property(x => x.LprDate)
            .HasColumnType("timestamp")
            .IsRequired();

        builder.Property(x => x.ImagePath)
            .HasColumnType("varchar")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Metadata)
            .HasColumnType(EfConstants.ColumnTypes.Json)
            .HasConversion(
                metadata => metadata.ToString(),
                metadata => JObject.Parse(metadata));

        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
