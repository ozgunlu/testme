using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DivitOtoyol.Modules.Locations.Shared.Data.Migrations.Locations
{
    /// <inheritdoc />
    public partial class InitialLocationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "location");

            migrationBuilder.CreateTable(
                name: "locations",
                schema: "location",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    parentid = table.Column<long>(name: "parent_id", type: "bigint", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    createdby = table.Column<int>(name: "created_by", type: "integer", nullable: true),
                    originalversion = table.Column<long>(name: "original_version", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locations", x => x.id);
                    table.ForeignKey(
                        name: "fk_locations_locations_parent_temp_id",
                        column: x => x.parentid,
                        principalSchema: "location",
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_locations_id",
                schema: "location",
                table: "locations",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_locations_parent_id",
                schema: "location",
                table: "locations",
                column: "parent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "locations",
                schema: "location");
        }
    }
}
