using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DivitOtoyol.Modules.Systems.Shared.Data.Migrations.Systems
{
    /// <inheritdoc />
    public partial class InitialSystemMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "system");

            migrationBuilder.CreateTable(
                name: "options",
                schema: "system",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    key = table.Column<string>(type: "varchar(50)", nullable: false),
                    value = table.Column<string>(type: "varchar(50)", nullable: false),
                    modules = table.Column<string>(type: "varchar(50)", nullable: false),
                    description = table.Column<string>(type: "varchar(50)", nullable: false),
                    canupdate = table.Column<bool>(name: "can_update", type: "boolean", nullable: false),
                    candelete = table.Column<bool>(name: "can_delete", type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    createdby = table.Column<int>(name: "created_by", type: "integer", nullable: true),
                    originalversion = table.Column<long>(name: "original_version", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_options", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_options_id",
                schema: "system",
                table: "options",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_options_key",
                schema: "system",
                table: "options",
                column: "key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "options",
                schema: "system");
        }
    }
}
