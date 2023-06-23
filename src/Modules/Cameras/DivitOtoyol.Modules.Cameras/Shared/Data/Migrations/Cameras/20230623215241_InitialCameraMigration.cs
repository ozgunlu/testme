using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DivitOtoyol.Modules.Cameras.Shared.Data.Migrations.Cameras
{
    /// <inheritdoc />
    public partial class InitialCameraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "camera");

            migrationBuilder.CreateTable(
                name: "cameras",
                schema: "camera",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    locationinformationname = table.Column<string>(name: "location_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    locationinformationid = table.Column<long>(name: "location_information_id", type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    biosname = table.Column<string>(name: "bios_name", type: "text", nullable: true),
                    serialnumber = table.Column<string>(name: "serial_number", type: "text", nullable: true),
                    ip = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    createdby = table.Column<int>(name: "created_by", type: "integer", nullable: true),
                    originalversion = table.Column<long>(name: "original_version", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cameras", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cameras_id",
                schema: "camera",
                table: "cameras",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cameras",
                schema: "camera");
        }
    }
}
