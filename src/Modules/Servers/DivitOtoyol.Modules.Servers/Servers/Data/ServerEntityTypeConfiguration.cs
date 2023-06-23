using BuildingBlocks.Core.Persistence.EfCore;
using DivitOtoyol.Modules.Servers.Servers.Models;
using DivitOtoyol.Modules.Servers.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivitOtoyol.Modules.Servers.Servers.Data;

public class ServerEntityTypeConfiguration : IEntityTypeConfiguration<Server>
{
    public void Configure(EntityTypeBuilder<Server> builder)
    {
        builder.ToTable("servers", ServerDbContext.DefaultSchema);

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

        builder.Property(x => x.Name)
            .HasColumnType(EfConstants.ColumnTypes.NormalText)
            .HasConversion(name => name.Value, name => ValueObjects.Name.Create(name))
            .IsRequired();

        builder.Property(x => x.Ip)
            .HasColumnType(EfConstants.ColumnTypes.NormalText)
            .IsRequired();

        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
