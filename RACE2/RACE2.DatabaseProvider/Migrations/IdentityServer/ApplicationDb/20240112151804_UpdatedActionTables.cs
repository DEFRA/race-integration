using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdatedActionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_AspNetUserRoles_OwnerRolecId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_AspNetUsers_OwnedByUserId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Reservoirs_ReservoirId",
                table: "Actions");

            migrationBuilder.DropIndex(
                name: "IX_Actions_OwnerRolecId",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "OwnerRolecId",
                table: "Actions");

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnedByUserId",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerRoleId",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Actions_OwnerRoleId",
                table: "Actions",
                column: "OwnerRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_AspNetUserRoles_OwnerRoleId",
                table: "Actions",
                column: "OwnerRoleId",
                principalTable: "AspNetUserRoles",
                principalColumn: "cId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_AspNetUsers_OwnedByUserId",
                table: "Actions",
                column: "OwnedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Reservoirs_ReservoirId",
                table: "Actions",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_AspNetUserRoles_OwnerRoleId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_AspNetUsers_OwnedByUserId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Reservoirs_ReservoirId",
                table: "Actions");

            migrationBuilder.DropIndex(
                name: "IX_Actions_OwnerRoleId",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "OwnerRoleId",
                table: "Actions");

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "Actions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OwnedByUserId",
                table: "Actions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OwnerRolecId",
                table: "Actions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actions_OwnerRolecId",
                table: "Actions",
                column: "OwnerRolecId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_AspNetUserRoles_OwnerRolecId",
                table: "Actions",
                column: "OwnerRolecId",
                principalTable: "AspNetUserRoles",
                principalColumn: "cId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_AspNetUsers_OwnedByUserId",
                table: "Actions",
                column: "OwnedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Reservoirs_ReservoirId",
                table: "Actions",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id");
        }
    }
}
