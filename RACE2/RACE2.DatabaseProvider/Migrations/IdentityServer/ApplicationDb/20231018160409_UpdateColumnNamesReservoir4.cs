using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateColumnNamesReservoir4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SecondaryContactId",
                table: "OrganisationReservoirs",
                type: "int",
                nullable: false,
                defaultValue: 20);

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationReservoirs_SecondaryContactId",
                table: "OrganisationReservoirs",
                column: "SecondaryContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_SecondaryContactId",
                table: "OrganisationReservoirs",
                column: "SecondaryContactId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_SecondaryContactId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationReservoirs_SecondaryContactId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropColumn(
                name: "SecondaryContactId",
                table: "OrganisationReservoirs");
        }
    }
}
