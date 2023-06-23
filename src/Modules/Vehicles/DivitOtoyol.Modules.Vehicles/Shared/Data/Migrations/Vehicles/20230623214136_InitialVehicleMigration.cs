using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DivitOtoyol.Modules.Vehicles.Shared.Data.Migrations.Vehicles
{
    /// <inheritdoc />
    public partial class InitialVehicleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "vehicle");

            migrationBuilder.CreateTable(
                name: "colors",
                schema: "vehicle",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    createdby = table.Column<int>(name: "created_by", type: "integer", nullable: true),
                    originalversion = table.Column<long>(name: "original_version", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_colors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "makes",
                schema: "vehicle",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    createdby = table.Column<int>(name: "created_by", type: "integer", nullable: true),
                    originalversion = table.Column<long>(name: "original_version", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_makes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "types",
                schema: "vehicle",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    parentid = table.Column<long>(name: "parent_id", type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    createdby = table.Column<int>(name: "created_by", type: "integer", nullable: true),
                    originalversion = table.Column<long>(name: "original_version", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_types", x => x.id);
                    table.ForeignKey(
                        name: "fk_types_types_parent_temp_id1",
                        column: x => x.parentid,
                        principalSchema: "vehicle",
                        principalTable: "types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "models",
                schema: "vehicle",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    makeid = table.Column<long>(name: "make_id", type: "bigint", nullable: false),
                    typeid = table.Column<long>(name: "type_id", type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    createdby = table.Column<int>(name: "created_by", type: "integer", nullable: true),
                    originalversion = table.Column<long>(name: "original_version", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_models", x => x.id);
                    table.ForeignKey(
                        name: "fk_models_makes_make_temp_id",
                        column: x => x.makeid,
                        principalSchema: "vehicle",
                        principalTable: "makes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_models_vehicle_types_type_temp_id",
                        column: x => x.typeid,
                        principalSchema: "vehicle",
                        principalTable: "types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_colors_id",
                schema: "vehicle",
                table: "colors",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_colors_name",
                schema: "vehicle",
                table: "colors",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_makes_id",
                schema: "vehicle",
                table: "makes",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_models_id",
                schema: "vehicle",
                table: "models",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_models_make_id",
                schema: "vehicle",
                table: "models",
                column: "make_id");

            migrationBuilder.CreateIndex(
                name: "ix_models_type_id_name",
                schema: "vehicle",
                table: "models",
                columns: new[] { "type_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_types_id",
                schema: "vehicle",
                table: "types",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_types_parent_id",
                schema: "vehicle",
                table: "types",
                column: "parent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "colors",
                schema: "vehicle");

            migrationBuilder.DropTable(
                name: "models",
                schema: "vehicle");

            migrationBuilder.DropTable(
                name: "makes",
                schema: "vehicle");

            migrationBuilder.DropTable(
                name: "types",
                schema: "vehicle");
        }
    }
}
