using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateCommentTables1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SafetyMeasures_Reservoirs_ReservoirId",
                table: "SafetyMeasures");

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "SafetyMeasures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SafetyMeasures_Reservoirs_ReservoirId",
                table: "SafetyMeasures",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SafetyMeasures_Reservoirs_ReservoirId",
                table: "SafetyMeasures");

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "SafetyMeasures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SafetyMeasures_Reservoirs_ReservoirId",
                table: "SafetyMeasures",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id");
        }
    }
}
