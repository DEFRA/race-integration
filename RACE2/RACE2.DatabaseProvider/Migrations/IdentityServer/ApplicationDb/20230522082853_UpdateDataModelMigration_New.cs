using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateDataModelMigration_New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationAddresses_Addresses_Addressesid",
                table: "OrganisationAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganisationAddresses",
                table: "OrganisationAddresses");

            migrationBuilder.RenameColumn(
                name: "Addressesid",
                table: "OrganisationAddresses",
                newName: "Addressid");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryContactId",
                table: "OrganisationReservoirs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrganisationAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "OrganisationAddresses",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganisationAddresses",
                table: "OrganisationAddresses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationReservoirs_PrimaryContactId",
                table: "OrganisationReservoirs",
                column: "PrimaryContactId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationAddresses_Addressid",
                table: "OrganisationAddresses",
                column: "Addressid");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationAddresses_Addresses_Addressid",
                table: "OrganisationAddresses",
                column: "Addressid",
                principalTable: "Addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_PrimaryContactId",
                table: "OrganisationReservoirs",
                column: "PrimaryContactId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationAddresses_Addresses_Addressid",
                table: "OrganisationAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_PrimaryContactId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationReservoirs_PrimaryContactId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganisationAddresses",
                table: "OrganisationAddresses");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationAddresses_Addressid",
                table: "OrganisationAddresses");

            migrationBuilder.DropColumn(
                name: "PrimaryContactId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrganisationAddresses");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "OrganisationAddresses");

            migrationBuilder.RenameColumn(
                name: "Addressid",
                table: "OrganisationAddresses",
                newName: "Addressesid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganisationAddresses",
                table: "OrganisationAddresses",
                columns: new[] { "Addressesid", "OrganisationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationAddresses_Addresses_Addressesid",
                table: "OrganisationAddresses",
                column: "Addressesid",
                principalTable: "Addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
