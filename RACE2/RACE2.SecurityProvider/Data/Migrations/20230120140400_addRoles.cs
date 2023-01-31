using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.SecurityProvider.Data.Migrations
{
    public partial class addRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "display_name",
                table: "AspNetRoles",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "parent_id",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "display_name",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "AspNetRoles",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }
    }
}
