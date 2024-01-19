using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class AddedReservoirChangeHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RAW_WatchItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RAW_StatementDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RAW_MIOS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RAW_MaintenanceMeasures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RAW_ActionSummary",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReservoirDetailsChangeHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservoirId = table.Column<int>(type: "int", nullable: false),
                    SourceSubmissionId = table.Column<int>(type: "int", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ChangeDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsBackEndChange = table.Column<bool>(type: "bit", nullable: false),
                    ChangeByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservoirDetailsChangeHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservoirDetailsChangeHistory_AspNetUsers_ChangeByUserId",
                        column: x => x.ChangeByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReservoirDetailsChangeHistory_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservoirDetailsChangeHistory_SubmissionStatus_SourceSubmissionId",
                        column: x => x.SourceSubmissionId,
                        principalTable: "SubmissionStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirDetailsChangeHistory_ChangeByUserId",
                table: "ReservoirDetailsChangeHistory",
                column: "ChangeByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirDetailsChangeHistory_ReservoirId",
                table: "ReservoirDetailsChangeHistory",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirDetailsChangeHistory_SourceSubmissionId",
                table: "ReservoirDetailsChangeHistory",
                column: "SourceSubmissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservoirDetailsChangeHistory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RAW_WatchItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RAW_StatementDetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RAW_MIOS");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RAW_MaintenanceMeasures");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RAW_ActionSummary");
        }
    }
}
