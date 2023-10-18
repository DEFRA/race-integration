using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateColumnNamesReservoir2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReservoirs_AspNetUsers_UserDetailId",
                table: "UserReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReservoirs_Reservoirs_ReservoirId",
                table: "UserReservoirs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReservoirs",
                table: "UserReservoirs");

            migrationBuilder.RenameTable(
                name: "UserReservoirs",
                newName: "UserReservoir");

            migrationBuilder.RenameIndex(
                name: "IX_UserReservoirs_UserDetailId",
                table: "UserReservoir",
                newName: "IX_UserReservoir_UserDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReservoirs_ReservoirId",
                table: "UserReservoir",
                newName: "IX_UserReservoir_ReservoirId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReservoir",
                table: "UserReservoir",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReservoir_AspNetUsers_UserDetailId",
                table: "UserReservoir",
                column: "UserDetailId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReservoir_Reservoirs_ReservoirId",
                table: "UserReservoir",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReservoir_AspNetUsers_UserDetailId",
                table: "UserReservoir");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReservoir_Reservoirs_ReservoirId",
                table: "UserReservoir");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReservoir",
                table: "UserReservoir");

            migrationBuilder.RenameTable(
                name: "UserReservoir",
                newName: "UserReservoirs");

            migrationBuilder.RenameIndex(
                name: "IX_UserReservoir_UserDetailId",
                table: "UserReservoirs",
                newName: "IX_UserReservoirs_UserDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReservoir_ReservoirId",
                table: "UserReservoirs",
                newName: "IX_UserReservoirs_ReservoirId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReservoirs",
                table: "UserReservoirs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReservoirs_AspNetUsers_UserDetailId",
                table: "UserReservoirs",
                column: "UserDetailId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReservoirs_Reservoirs_ReservoirId",
                table: "UserReservoirs",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
