using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateColumnNamesReservoir3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastInspectionByUserId",
                table: "Reservoirs",
                type: "int",
                nullable: false,
                defaultValue: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_LastInspectionByUserId",
                table: "Reservoirs",
                column: "LastInspectionByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_AspNetUsers_LastInspectionByUserId",
                table: "Reservoirs",
                column: "LastInspectionByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_AspNetUsers_LastInspectionByUserId",
                table: "Reservoirs");

            migrationBuilder.DropIndex(
                name: "IX_Reservoirs_LastInspectionByUserId",
                table: "Reservoirs");

            migrationBuilder.DropColumn(
                name: "LastInspectionByUserId",
                table: "Reservoirs");
        }
    }
}
