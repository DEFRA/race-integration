using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DataAccess.Migrations.IdentityServer.ApplicationDb
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Organisations_OrganisationIdid",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationAddresses_Organisations_Organisationid",
                table: "OrganisationAddresses");

            migrationBuilder.DropColumn(
                name: "c_end_date",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "c_start_date",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "AddressLine3",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "NearestPostcode",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "NearestTown",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Organisations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Organisationid",
                table: "OrganisationAddresses",
                newName: "OrganisationId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationAddresses_Organisationid",
                table: "OrganisationAddresses",
                newName: "IX_OrganisationAddresses_OrganisationId");

            migrationBuilder.RenameColumn(
                name: "OrganisationIdid",
                table: "AspNetUsers",
                newName: "OrganisationId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_OrganisationIdid",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_OrganisationId");

            migrationBuilder.RenameColumn(
                name: "c_parent_id",
                table: "AspNetRoles",
                newName: "c_parent_roleid");

            migrationBuilder.AddColumn<string>(
                name: "NearestTown",
                table: "Reservoirs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "c_parent_userid",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Organisations_OrganisationId",
                table: "AspNetUsers",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationAddresses_Organisations_OrganisationId",
                table: "OrganisationAddresses",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Organisations_OrganisationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationAddresses_Organisations_OrganisationId",
                table: "OrganisationAddresses");

            migrationBuilder.DropColumn(
                name: "NearestTown",
                table: "Reservoirs");

            migrationBuilder.DropColumn(
                name: "c_parent_userid",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Organisations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OrganisationId",
                table: "OrganisationAddresses",
                newName: "Organisationid");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationAddresses_OrganisationId",
                table: "OrganisationAddresses",
                newName: "IX_OrganisationAddresses_Organisationid");

            migrationBuilder.RenameColumn(
                name: "OrganisationId",
                table: "AspNetUsers",
                newName: "OrganisationIdid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_OrganisationId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_OrganisationIdid");

            migrationBuilder.RenameColumn(
                name: "c_parent_roleid",
                table: "AspNetRoles",
                newName: "c_parent_id");

            migrationBuilder.AddColumn<DateTime>(
                name: "c_end_date",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "c_start_date",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AddressLine3",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NearestPostcode",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NearestTown",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Organisations_OrganisationIdid",
                table: "AspNetUsers",
                column: "OrganisationIdid",
                principalTable: "Organisations",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationAddresses_Organisations_Organisationid",
                table: "OrganisationAddresses",
                column: "Organisationid",
                principalTable: "Organisations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
