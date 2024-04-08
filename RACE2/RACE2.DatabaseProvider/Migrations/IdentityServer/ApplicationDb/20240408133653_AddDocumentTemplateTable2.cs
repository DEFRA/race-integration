using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddDocumentTemplateTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTemplate_AspNetUsers_UserId",
                table: "DocumentTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTemplate_FeatureFunction_ServiceId",
                table: "DocumentTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTemplate_Organisation_OrganisationId",
                table: "DocumentTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTemplate_Reservoirs_ReservoirId",
                table: "DocumentTemplate");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DocumentTemplate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "DocumentTemplate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "DocumentTemplate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrganisationId",
                table: "DocumentTemplate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTemplate_AspNetUsers_UserId",
                table: "DocumentTemplate",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTemplate_FeatureFunction_ServiceId",
                table: "DocumentTemplate",
                column: "ServiceId",
                principalTable: "FeatureFunction",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTemplate_Organisation_OrganisationId",
                table: "DocumentTemplate",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTemplate_Reservoirs_ReservoirId",
                table: "DocumentTemplate",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTemplate_AspNetUsers_UserId",
                table: "DocumentTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTemplate_FeatureFunction_ServiceId",
                table: "DocumentTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTemplate_Organisation_OrganisationId",
                table: "DocumentTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTemplate_Reservoirs_ReservoirId",
                table: "DocumentTemplate");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DocumentTemplate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "DocumentTemplate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "DocumentTemplate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganisationId",
                table: "DocumentTemplate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTemplate_AspNetUsers_UserId",
                table: "DocumentTemplate",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTemplate_FeatureFunction_ServiceId",
                table: "DocumentTemplate",
                column: "ServiceId",
                principalTable: "FeatureFunction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTemplate_Organisation_OrganisationId",
                table: "DocumentTemplate",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTemplate_Reservoirs_ReservoirId",
                table: "DocumentTemplate",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
