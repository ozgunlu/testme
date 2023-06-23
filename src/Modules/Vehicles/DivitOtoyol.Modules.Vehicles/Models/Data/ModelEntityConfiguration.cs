using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Vehicles.Models.Models;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivitOtoyol.Modules.Vehicles.Models.Data;

public class ModelEntityConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.ToTable("models", VehicleDbContext.DefaultSchema);
        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, id => id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name).HasColumnType(EfConstants.ColumnTypes.NormalText).IsRequired();

        builder.Property(x => x.MakeId)
            .HasConversion(makeId => makeId.Value, makeId => makeId);

        builder.HasOne(x => x.Make)
            .WithMany()
            .HasForeignKey(x => (long)x.MakeId);

        builder.Property(x => x.TypeId)
            .HasConversion(typeId => typeId.Value, typeId => typeId);

        builder.HasOne(x => x.Type)
            .WithMany()
            .HasForeignKey(x => (long)x.TypeId);

        // Add a composite unique constraint on TypeId and Name
        builder.HasIndex(x => new { x.TypeId, x.Name }).IsUnique();

        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
