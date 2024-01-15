using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdatedActionTables3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_AspNetUserRoles_OwnerRoleId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_AspNetUsers_OwnedByUserId",
                table: "Actions");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerRoleId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_AspNetUserRoles_OwnerRoleId",
                table: "Actions",
                column: "OwnerRoleId",
                principalTable: "AspNetUserRoles",
                principalColumn: "cId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_AspNetUsers_OwnedByUserId",
                table: "Actions",
                column: "OwnedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_AspNetUserRoles_OwnerRoleId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_AspNetUsers_OwnedByUserId",
                table: "Actions");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerRoleId",
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
