using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Data.Migrations.PlateRecognitions
{
    /// <inheritdoc />
    public partial class InitialPlateRecognitionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "plate_recognition");

            migrationBuilder.CreateTable(
                name: "records",
                schema: "plate_recognition",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    plate = table.Column<string>(type: "varchar(50)", nullable: false),
                    camerainformationname = table.Column<string>(name: "camera_information_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    camerainformationid = table.Column<long>(name: "camera_information_id", type: "bigint", nullable: false),
                    makeinformationname = table.Column<string>(name: "make_information_name", type: "character varying(50)", maxLength: 50, nullable: true),
                    makeinformationid = table.Column<long>(name: "make_information_id", type: "bigint", nullable: true),
                    modelinformationname = table.Column<string>(name: "model_information_name", type: "character varying(50)", maxLength: 50, nullable: true),
                    modelinformationid = table.Column<long>(name: "model_information_id", type: "bigint", nullable: true),
                    modelinformationtypename = table.Column<string>(name: "model_information_type_name", type: "text", nullable: true),
                    colorinformationname = table.Column<string>(name: "color_information_name", type: "character varying(50)", maxLength: 50, nullable: true),
                    colorinformationid = table.Column<long>(name: "color_information_id", type: "bigint", nullable: true),
                    lprdate = table.Column<DateTime>(name: "lpr_date", type: "timestamp", nullable: false),
                    imagepath = table.Column<string>(name: "image_path", type: "varchar", maxLength: 255, nullable: false),
                    metadata = table.Column<string>(type: "json", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    createdby = table.Column<int>(name: "created_by", type: "integer", nullable: true),
                    originalversion = table.Column<long>(name: "original_version", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_records", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_records_id",
                schema: "plate_recognition",
                table: "records",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "records",
                schema: "plate_recognition");
        }
    }
}
