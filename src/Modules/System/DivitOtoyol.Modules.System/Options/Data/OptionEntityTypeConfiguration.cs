using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Systems.Options.Models;
using DivitOtoyol.Modules.Systems.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivitOtoyol.Modules.Systems.Options.Data;

public class OptionEntityTypeConfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.ToTable("options", SystemDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, id => id)
            .ValueGeneratedNever();

        builder.Property(x => x.Key).HasColumnType(EfConstants.ColumnTypes.NormalText).IsRequired();
        builder.HasIndex(x => x.Key).IsUnique();

        builder.Property(x => x.Value).HasColumnType(EfConstants.ColumnTypes.NormalText).IsRequired();
        builder.Property(x => x.Modules).HasColumnType(EfConstants.ColumnTypes.NormalText).IsRequired();
        builder.Property(x => x.Description).HasColumnType(EfConstants.ColumnTypes.NormalText);
        builder.Property(x => x.CanUpdate).IsRequired();
        builder.Property(x => x.CanDelete).IsRequired();
        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
