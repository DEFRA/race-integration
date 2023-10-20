using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateFKConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_PrimaryContactId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_SecondaryContactId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_AspNetUsers_LastInspectionByUserId",
                table: "Reservoirs");

            migrationBuilder.AlterColumn<int>(
                name: "LastInspectionByUserId",
                table: "Reservoirs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SecondaryContactId",
                table: "OrganisationReservoirs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PrimaryContactId",
                table: "OrganisationReservoirs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_PrimaryContactId",
                table: "OrganisationReservoirs",
                column: "PrimaryContactId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_SecondaryContactId",
                table: "OrganisationReservoirs",
                column: "SecondaryContactId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_AspNetUsers_LastInspectionByUserId",
                table: "Reservoirs",
                column: "LastInspectionByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_PrimaryContactId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_SecondaryContactId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_AspNetUsers_LastInspectionByUserId",
                table: "Reservoirs");

            migrationBuilder.AlterColumn<int>(
                name: "LastInspectionByUserId",
                table: "Reservoirs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SecondaryContactId",
                table: "OrganisationReservoirs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrimaryContactId",
                table: "OrganisationReservoirs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_PrimaryContactId",
                table: "OrganisationReservoirs",
                column: "PrimaryContactId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_SecondaryContactId",
                table: "OrganisationReservoirs",
                column: "SecondaryContactId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_AspNetUsers_LastInspectionByUserId",
                table: "Reservoirs",
                column: "LastInspectionByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
