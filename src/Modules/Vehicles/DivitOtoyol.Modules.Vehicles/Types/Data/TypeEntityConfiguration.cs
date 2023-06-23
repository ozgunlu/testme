using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleType = DivitOtoyol.Modules.Vehicles.Types.Models.Type;

namespace DivitOtoyol.Modules.Vehicles.Types.Data;

public class TypeEntityConfiguration : IEntityTypeConfiguration<VehicleType>
{
    public void Configure(EntityTypeBuilder<VehicleType> builder)
    {
        builder.ToTable("types", VehicleDbContext.DefaultSchema);
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
