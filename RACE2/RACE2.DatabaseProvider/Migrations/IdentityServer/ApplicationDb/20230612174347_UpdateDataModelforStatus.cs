using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateDataModelforStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "SubmissionStatus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "SubmissionStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLegacySubmission",
                table: "SubmissionStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "SubmissionStatus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedScreen",
                table: "SubmissionStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "SubmissionStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservoirId",
                table: "SubmissionStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "SubmissionStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SubmissionStatus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubmittedBy",
                table: "SubmissionStatus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedOn",
                table: "SubmissionStatus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ScreenDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    HasSignificantChange = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenDefinition_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionStatus_ModifiedById",
                table: "SubmissionStatus",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionStatus_ReservoirId",
                table: "SubmissionStatus",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionStatus_ServiceId",
                table: "SubmissionStatus",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenDefinition_ModifiedById",
                table: "ScreenDefinition",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionStatus_AspNetUsers_ModifiedById",
                table: "SubmissionStatus",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionStatus_FeatureFunctions_ServiceId",
                table: "SubmissionStatus",
                column: "ServiceId",
                principalTable: "FeatureFunctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionStatus_Reservoirs_ReservoirId",
                table: "SubmissionStatus",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionStatus_AspNetUsers_ModifiedById",
                table: "SubmissionStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionStatus_FeatureFunctions_ServiceId",
                table: "SubmissionStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionStatus_Reservoirs_ReservoirId",
                table: "SubmissionStatus");

            migrationBuilder.DropTable(
                name: "ScreenDefinition");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionStatus_ModifiedById",
                table: "SubmissionStatus");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionStatus_ReservoirId",
                table: "SubmissionStatus");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionStatus_ServiceId",
                table: "SubmissionStatus");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "SubmissionStatus");

            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "SubmissionStatus");

            migrationBuilder.DropColumn(
                name: "IsLegacySubmission",
                table: "SubmissionStatus");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "SubmissionStatus");

            migrationBuilder.DropColumn(
                name: "LastModifiedScreen",
                table: "SubmissionStatus");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "SubmissionStatus");

            migrationBuilder.DropColumn(
                name: "ReservoirId",
                table: "SubmissionStatus");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "SubmissionStatus");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubmissionStatus");

            migrationBuilder.DropColumn(
                name: "SubmittedBy",
                table: "SubmissionStatus");

            migrationBuilder.DropColumn(
                name: "SubmittedOn",
                table: "SubmissionStatus");
        }
    }
}
