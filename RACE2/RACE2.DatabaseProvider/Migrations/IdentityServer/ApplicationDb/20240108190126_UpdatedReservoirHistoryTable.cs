using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdatedReservoirHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservoirDetailsChangeHistory_AspNetUsers_ChangeByUserId",
                table: "ReservoirDetailsChangeHistory");

            migrationBuilder.AlterColumn<string>(
                name: "NearestTown",
                table: "Reservoirs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChangeByUserId",
                table: "ReservoirDetailsChangeHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservoirDetailsChangeHistory_AspNetUsers_ChangeByUserId",
                table: "ReservoirDetailsChangeHistory",
                column: "ChangeByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservoirDetailsChangeHistory_AspNetUsers_ChangeByUserId",
                table: "ReservoirDetailsChangeHistory");

            migrationBuilder.AlterColumn<string>(
                name: "NearestTown",
                table: "Reservoirs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChangeByUserId",
                table: "ReservoirDetailsChangeHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservoirDetailsChangeHistory_AspNetUsers_ChangeByUserId",
                table: "ReservoirDetailsChangeHistory",
                column: "ChangeByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
