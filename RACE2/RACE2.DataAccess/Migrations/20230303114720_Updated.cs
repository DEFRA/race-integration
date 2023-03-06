using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DataAccess.Migrations
{
    public partial class Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "default_value",
                table: "FeatureFunctions");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "UserPermissions",
                newName: "Start_date");

            migrationBuilder.RenameColumn(
                name: "end_date",
                table: "UserPermissions",
                newName: "End_date");

            migrationBuilder.RenameColumn(
                name: "access_level",
                table: "UserPermissions",
                newName: "Access_level");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserPermissions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "FeatureFunctions",
                newName: "Start_date");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "FeatureFunctions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "end_date",
                table: "FeatureFunctions",
                newName: "End_date");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "FeatureFunctions",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "display_name",
                table: "FeatureFunctions",
                newName: "DisplayName");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Start_date",
                table: "UserPermissions",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "End_date",
                table: "UserPermissions",
                newName: "end_date");

            migrationBuilder.RenameColumn(
                name: "Access_level",
                table: "UserPermissions",
                newName: "access_level");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserPermissions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Start_date",
                table: "FeatureFunctions",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FeatureFunctions",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "End_date",
                table: "FeatureFunctions",
                newName: "end_date");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "FeatureFunctions",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "FeatureFunctions",
                newName: "display_name");

            migrationBuilder.AddColumn<string>(
                name: "default_value",
                table: "FeatureFunctions",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }
    }
}
