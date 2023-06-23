﻿// <auto-generated />
using System;
using DivitOtoyol.Modules.Systems.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DivitOtoyol.Modules.Systems.Shared.Data.Migrations.Systems
{
    [DbContext(typeof(SystemDbContext))]
    partial class SystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DivitOtoyol.Modules.Systems.Options.Models.Option", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<bool>("CanDelete")
                        .HasColumnType("boolean")
                        .HasColumnName("can_delete");

                    b.Property<bool>("CanUpdate")
                        .HasColumnType("boolean")
                        .HasColumnName("can_update");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created")
                        .HasDefaultValueSql("now()");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("created_by");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("description");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("key");

                    b.Property<string>("Modules")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("modules");

                    b.Property<long>("OriginalVersion")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint")
                        .HasColumnName("original_version");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_options");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("ix_options_id");

                    b.HasIndex("Key")
                        .IsUnique()
                        .HasDatabaseName("ix_options_key");

                    b.ToTable("options", "system");
                });
#pragma warning restore 612, 618
        }
    }
}
