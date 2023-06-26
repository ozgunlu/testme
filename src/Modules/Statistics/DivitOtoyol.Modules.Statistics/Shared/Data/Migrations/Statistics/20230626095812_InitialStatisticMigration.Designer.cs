﻿// <auto-generated />
using System;
using DivitOtoyol.Modules.Statistics.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DivitOtoyol.Modules.Statistics.Shared.Data.Migrations.Statistics
{
    [DbContext(typeof(StatisticDbContext))]
    [Migration("20230626095812_InitialStatisticMigration")]
    partial class InitialStatisticMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DivitOtoyol.Modules.Statistics.CameraStatistics.Models.CameraStatistic", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("FirstSeenAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("first_seen_at");

                    b.Property<DateTime>("LastSeenAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_seen_at");

                    b.Property<long>("OriginalVersion")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint")
                        .HasColumnName("original_version");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("plate");

                    b.Property<long>("TotalPassages")
                        .HasColumnType("bigint")
                        .HasColumnName("total_passages");

                    b.HasKey("Id")
                        .HasName("pk_camera_statistics");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("ix_camera_statistics_id");

                    b.ToTable("camera_statistics", "statistic");
                });

            modelBuilder.Entity("DivitOtoyol.Modules.Statistics.LocationStatistics.Models.LocationStatistic", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("FirstSeenAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("first_seen_at");

                    b.Property<DateTime>("LastSeenAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_seen_at");

                    b.Property<long>("OriginalVersion")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint")
                        .HasColumnName("original_version");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("plate");

                    b.Property<long>("TotalPassages")
                        .HasColumnType("bigint")
                        .HasColumnName("total_passages");

                    b.HasKey("Id")
                        .HasName("pk_location_statistics");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("ix_location_statistics_id");

                    b.ToTable("location_statistics", "statistic");
                });

            modelBuilder.Entity("DivitOtoyol.Modules.Statistics.PlateStatistics.Models.PlateStatistic", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("FirstSeenAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("first_seen_at");

                    b.Property<DateTime>("LastSeenAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_seen_at");

                    b.Property<long>("OriginalVersion")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint")
                        .HasColumnName("original_version");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("plate");

                    b.Property<long>("TotalPassages")
                        .HasColumnType("bigint")
                        .HasColumnName("total_passages");

                    b.HasKey("Id")
                        .HasName("pk_plate_statistics");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("ix_plate_statistics_id");

                    b.ToTable("plate_statistics", "statistic");
                });

            modelBuilder.Entity("DivitOtoyol.Modules.Statistics.CameraStatistics.Models.CameraStatistic", b =>
                {
                    b.OwnsOne("DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects.CameraInformation", "CameraInformation", b1 =>
                        {
                            b1.Property<long>("CameraStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("camera_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("camera_information_name");

                            b1.HasKey("CameraStatisticId");

                            b1.ToTable("camera_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("CameraStatisticId")
                                .HasConstraintName("fk_camera_statistics_camera_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects.ColorInformation", "ColorInformation", b1 =>
                        {
                            b1.Property<long>("CameraStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("color_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("color_information_name");

                            b1.HasKey("CameraStatisticId");

                            b1.ToTable("camera_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("CameraStatisticId")
                                .HasConstraintName("fk_camera_statistics_camera_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects.LocationInformation", "LocationInformation", b1 =>
                        {
                            b1.Property<long>("CameraStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("location_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("location_information_name");

                            b1.HasKey("CameraStatisticId");

                            b1.ToTable("camera_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("CameraStatisticId")
                                .HasConstraintName("fk_camera_statistics_camera_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects.MakeInformation", "MakeInformation", b1 =>
                        {
                            b1.Property<long>("CameraStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("make_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("make_information_name");

                            b1.HasKey("CameraStatisticId");

                            b1.ToTable("camera_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("CameraStatisticId")
                                .HasConstraintName("fk_camera_statistics_camera_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects.ModelInformation", "ModelInformation", b1 =>
                        {
                            b1.Property<long>("CameraStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("model_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("model_information_name");

                            b1.HasKey("CameraStatisticId");

                            b1.ToTable("camera_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("CameraStatisticId")
                                .HasConstraintName("fk_camera_statistics_camera_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.CameraStatistics.ValueObjects.TypeInformation", "TypeInformation", b1 =>
                        {
                            b1.Property<long>("CameraStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("type_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("type_information_name");

                            b1.HasKey("CameraStatisticId");

                            b1.ToTable("camera_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("CameraStatisticId")
                                .HasConstraintName("fk_camera_statistics_camera_statistics_id");
                        });

                    b.Navigation("CameraInformation")
                        .IsRequired();

                    b.Navigation("ColorInformation")
                        .IsRequired();

                    b.Navigation("LocationInformation")
                        .IsRequired();

                    b.Navigation("MakeInformation")
                        .IsRequired();

                    b.Navigation("ModelInformation")
                        .IsRequired();

                    b.Navigation("TypeInformation")
                        .IsRequired();
                });

            modelBuilder.Entity("DivitOtoyol.Modules.Statistics.LocationStatistics.Models.LocationStatistic", b =>
                {
                    b.OwnsOne("DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects.CameraInformation", "CameraInformation", b1 =>
                        {
                            b1.Property<long>("LocationStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("camera_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("camera_information_name");

                            b1.HasKey("LocationStatisticId");

                            b1.ToTable("location_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("LocationStatisticId")
                                .HasConstraintName("fk_location_statistics_location_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects.ColorInformation", "ColorInformation", b1 =>
                        {
                            b1.Property<long>("LocationStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("color_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("color_information_name");

                            b1.HasKey("LocationStatisticId");

                            b1.ToTable("location_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("LocationStatisticId")
                                .HasConstraintName("fk_location_statistics_location_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects.LocationInformation", "LocationInformation", b1 =>
                        {
                            b1.Property<long>("LocationStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("location_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("location_information_name");

                            b1.HasKey("LocationStatisticId");

                            b1.ToTable("location_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("LocationStatisticId")
                                .HasConstraintName("fk_location_statistics_location_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects.MakeInformation", "MakeInformation", b1 =>
                        {
                            b1.Property<long>("LocationStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("make_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("make_information_name");

                            b1.HasKey("LocationStatisticId");

                            b1.ToTable("location_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("LocationStatisticId")
                                .HasConstraintName("fk_location_statistics_location_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects.ModelInformation", "ModelInformation", b1 =>
                        {
                            b1.Property<long>("LocationStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("model_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("model_information_name");

                            b1.HasKey("LocationStatisticId");

                            b1.ToTable("location_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("LocationStatisticId")
                                .HasConstraintName("fk_location_statistics_location_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.LocationStatistics.ValueObjects.TypeInformation", "TypeInformation", b1 =>
                        {
                            b1.Property<long>("LocationStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("type_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("type_information_name");

                            b1.HasKey("LocationStatisticId");

                            b1.ToTable("location_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("LocationStatisticId")
                                .HasConstraintName("fk_location_statistics_location_statistics_id");
                        });

                    b.Navigation("CameraInformation")
                        .IsRequired();

                    b.Navigation("ColorInformation")
                        .IsRequired();

                    b.Navigation("LocationInformation")
                        .IsRequired();

                    b.Navigation("MakeInformation")
                        .IsRequired();

                    b.Navigation("ModelInformation")
                        .IsRequired();

                    b.Navigation("TypeInformation")
                        .IsRequired();
                });

            modelBuilder.Entity("DivitOtoyol.Modules.Statistics.PlateStatistics.Models.PlateStatistic", b =>
                {
                    b.OwnsOne("DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects.CameraInformation", "CameraInformation", b1 =>
                        {
                            b1.Property<long>("PlateStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("camera_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("camera_information_name");

                            b1.HasKey("PlateStatisticId");

                            b1.ToTable("plate_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("PlateStatisticId")
                                .HasConstraintName("fk_plate_statistics_plate_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects.ColorInformation", "ColorInformation", b1 =>
                        {
                            b1.Property<long>("PlateStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("color_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("color_information_name");

                            b1.HasKey("PlateStatisticId");

                            b1.ToTable("plate_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("PlateStatisticId")
                                .HasConstraintName("fk_plate_statistics_plate_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects.LocationInformation", "LocationInformation", b1 =>
                        {
                            b1.Property<long>("PlateStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("location_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("location_information_name");

                            b1.HasKey("PlateStatisticId");

                            b1.ToTable("plate_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("PlateStatisticId")
                                .HasConstraintName("fk_plate_statistics_plate_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects.MakeInformation", "MakeInformation", b1 =>
                        {
                            b1.Property<long>("PlateStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("make_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("make_information_name");

                            b1.HasKey("PlateStatisticId");

                            b1.ToTable("plate_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("PlateStatisticId")
                                .HasConstraintName("fk_plate_statistics_plate_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects.ModelInformation", "ModelInformation", b1 =>
                        {
                            b1.Property<long>("PlateStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("model_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("model_information_name");

                            b1.HasKey("PlateStatisticId");

                            b1.ToTable("plate_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("PlateStatisticId")
                                .HasConstraintName("fk_plate_statistics_plate_statistics_id");
                        });

                    b.OwnsOne("DivitOtoyol.Modules.Statistics.PlateStatistics.ValueObjects.TypeInformation", "TypeInformation", b1 =>
                        {
                            b1.Property<long>("PlateStatisticId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<long>("Id")
                                .HasColumnType("bigint")
                                .HasColumnName("type_information_id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("type_information_name");

                            b1.HasKey("PlateStatisticId");

                            b1.ToTable("plate_statistics", "statistic");

                            b1.WithOwner()
                                .HasForeignKey("PlateStatisticId")
                                .HasConstraintName("fk_plate_statistics_plate_statistics_id");
                        });

                    b.Navigation("CameraInformation")
                        .IsRequired();

                    b.Navigation("ColorInformation")
                        .IsRequired();

                    b.Navigation("LocationInformation")
                        .IsRequired();

                    b.Navigation("MakeInformation")
                        .IsRequired();

                    b.Navigation("ModelInformation")
                        .IsRequired();

                    b.Navigation("TypeInformation")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
