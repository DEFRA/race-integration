using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateColumnNamesReservoir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NextInspectionDate",
                table: "Reservoirs",
                newName: "NextInspectionDate103");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCertificationDate",
                table: "Reservoirs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastInspectionEngineerName",
                table: "Reservoirs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastInspectionEngineerPhone",
                table: "Reservoirs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextInspectionDate102",
                table: "Reservoirs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCertificationDate",
                table: "Reservoirs");

            migrationBuilder.DropColumn(
                name: "LastInspectionEngineerName",
                table: "Reservoirs");

            migrationBuilder.DropColumn(
                name: "LastInspectionEngineerPhone",
                table: "Reservoirs");

            migrationBuilder.DropColumn(
                name: "NextInspectionDate102",
                table: "Reservoirs");

            migrationBuilder.RenameColumn(
                name: "NextInspectionDate103",
                table: "Reservoirs",
                newName: "NextInspectionDate");
        }
    }
}
