using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DivitOtoyol.Modules.Servers.Shared.Data.Migrations.Servers
{
    /// <inheritdoc />
    public partial class InitialServerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "server");

            migrationBuilder.CreateTable(
                name: "servers",
                schema: "server",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    locationid = table.Column<long>(name: "location_id", type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    ip = table.Column<string>(type: "varchar(50)", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    createdby = table.Column<int>(name: "created_by", type: "integer", nullable: true),
                    originalversion = table.Column<long>(name: "original_version", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_servers", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_servers_id",
                schema: "server",
                table: "servers",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "servers",
                schema: "server");
        }
    }
}
