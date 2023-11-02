using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateMIOSAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Reservoirs_ReservoirId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_EarlyInspections_Reservoirs_ReservoirId",
                table: "EarlyInspections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EarlyInspections",
                table: "EarlyInspections");

            migrationBuilder.RenameTable(
                name: "EarlyInspections",
                newName: "EarlyInspection");

            migrationBuilder.RenameColumn(
                name: "Targetdate",
                table: "SafetyMeasures",
                newName: "TargetDate");

            migrationBuilder.RenameColumn(
                name: "Createddate",
                table: "SafetyMeasures",
                newName: "CreatedDate");

            migrationBuilder.RenameIndex(
                name: "IX_EarlyInspections_ReservoirId",
                table: "EarlyInspection",
                newName: "IX_EarlyInspection_ReservoirId");

            migrationBuilder.AlterColumn<string>(
                name: "RelatesToObject",
                table: "Comments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "Actions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsMandatory",
                table: "Actions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Actions",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EarlyInspection",
                table: "EarlyInspection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Reservoirs_ReservoirId",
                table: "Actions",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EarlyInspection_Reservoirs_ReservoirId",
                table: "EarlyInspection",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Reservoirs_ReservoirId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_EarlyInspection_Reservoirs_ReservoirId",
                table: "EarlyInspection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EarlyInspection",
                table: "EarlyInspection");

            migrationBuilder.DropColumn(
                name: "IsMandatory",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Actions");

            migrationBuilder.RenameTable(
                name: "EarlyInspection",
                newName: "EarlyInspections");

            migrationBuilder.RenameColumn(
                name: "TargetDate",
                table: "SafetyMeasures",
                newName: "Targetdate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "SafetyMeasures",
                newName: "Createddate");

            migrationBuilder.RenameIndex(
                name: "IX_EarlyInspection_ReservoirId",
                table: "EarlyInspections",
                newName: "IX_EarlyInspections_ReservoirId");

            migrationBuilder.AlterColumn<string>(
                name: "RelatesToObject",
                table: "Comments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EarlyInspections",
                table: "EarlyInspections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Reservoirs_ReservoirId",
                table: "Actions",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EarlyInspections_Reservoirs_ReservoirId",
                table: "EarlyInspections",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
