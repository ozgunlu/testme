using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DivitOtoyol.Modules.Statistics.Shared.Data.Migrations.Statistics
{
    /// <inheritdoc />
    public partial class InitialStatisticMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "statistic");

            migrationBuilder.CreateTable(
                name: "camera_statistics",
                schema: "statistic",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    locationinformationname = table.Column<string>(name: "location_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    locationinformationid = table.Column<long>(name: "location_information_id", type: "bigint", nullable: false),
                    camerainformationname = table.Column<string>(name: "camera_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    camerainformationid = table.Column<long>(name: "camera_information_id", type: "bigint", nullable: false),
                    typeinformationname = table.Column<string>(name: "type_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    typeinformationid = table.Column<long>(name: "type_information_id", type: "bigint", nullable: false),
                    makeinformationname = table.Column<string>(name: "make_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    makeinformationid = table.Column<long>(name: "make_information_id", type: "bigint", nullable: false),
                    modelinformationname = table.Column<string>(name: "model_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    modelinformationid = table.Column<long>(name: "model_information_id", type: "bigint", nullable: false),
                    colorinformationname = table.Column<string>(name: "color_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    colorinformationid = table.Column<long>(name: "color_information_id", type: "bigint", nullable: false),
                    plate = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    totalpassages = table.Column<long>(name: "total_passages", type: "bigint", nullable: false),
                    firstseenat = table.Column<DateTime>(name: "first_seen_at", type: "timestamp with time zone", nullable: false),
                    lastseenat = table.Column<DateTime>(name: "last_seen_at", type: "timestamp with time zone", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<int>(name: "created_by", type: "integer", nullable: true),
                    originalversion = table.Column<long>(name: "original_version", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_camera_statistics", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "location_statistics",
                schema: "statistic",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    locationinformationname = table.Column<string>(name: "location_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    locationinformationid = table.Column<long>(name: "location_information_id", type: "bigint", nullable: false),
                    camerainformationname = table.Column<string>(name: "camera_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    camerainformationid = table.Column<long>(name: "camera_information_id", type: "bigint", nullable: false),
                    typeinformationname = table.Column<string>(name: "type_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    typeinformationid = table.Column<long>(name: "type_information_id", type: "bigint", nullable: false),
                    makeinformationname = table.Column<string>(name: "make_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    makeinformationid = table.Column<long>(name: "make_information_id", type: "bigint", nullable: false),
                    modelinformationname = table.Column<string>(name: "model_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    modelinformationid = table.Column<long>(name: "model_information_id", type: "bigint", nullable: false),
                    colorinformationname = table.Column<string>(name: "color_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    colorinformationid = table.Column<long>(name: "color_information_id", type: "bigint", nullable: false),
                    plate = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    totalpassages = table.Column<long>(name: "total_passages", type: "bigint", nullable: false),
                    firstseenat = table.Column<DateTime>(name: "first_seen_at", type: "timestamp with time zone", nullable: false),
                    lastseenat = table.Column<DateTime>(name: "last_seen_at", type: "timestamp with time zone", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<int>(name: "created_by", type: "integer", nullable: true),
                    originalversion = table.Column<long>(name: "original_version", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_location_statistics", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "plate_statistics",
                schema: "statistic",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    locationinformationname = table.Column<string>(name: "location_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    locationinformationid = table.Column<long>(name: "location_information_id", type: "bigint", nullable: false),
                    camerainformationname = table.Column<string>(name: "camera_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    camerainformationid = table.Column<long>(name: "camera_information_id", type: "bigint", nullable: false),
                    typeinformationname = table.Column<string>(name: "type_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    typeinformationid = table.Column<long>(name: "type_information_id", type: "bigint", nullable: false),
                    makeinformationname = table.Column<string>(name: "make_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    makeinformationid = table.Column<long>(name: "make_information_id", type: "bigint", nullable: false),
                    modelinformationname = table.Column<string>(name: "model_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    modelinformationid = table.Column<long>(name: "model_information_id", type: "bigint", nullable: false),
                    colorinformationname = table.Column<string>(name: "color_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    colorinformationid = table.Column<long>(name: "color_information_id", type: "bigint", nullable: false),
                    plate = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    totalpassages = table.Column<long>(name: "total_passages", type: "bigint", nullable: false),
                    firstseenat = table.Column<DateTime>(name: "first_seen_at", type: "timestamp with time zone", nullable: false),
                    lastseenat = table.Column<DateTime>(name: "last_seen_at", type: "timestamp with time zone", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<int>(name: "created_by", type: "integer", nullable: true),
                    originalversion = table.Column<long>(name: "original_version", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_plate_statistics", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_camera_statistics_id",
                schema: "statistic",
                table: "camera_statistics",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_location_statistics_id",
                schema: "statistic",
                table: "location_statistics",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_plate_statistics_id",
                schema: "statistic",
                table: "plate_statistics",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "camera_statistics",
                schema: "statistic");

            migrationBuilder.DropTable(
                name: "location_statistics",
                schema: "statistic");

            migrationBuilder.DropTable(
                name: "plate_statistics",
                schema: "statistic");
        }
    }
}
