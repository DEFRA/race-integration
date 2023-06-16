using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateDataModelforScreen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScreenSequenceAuditHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ChangeEvent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValue = table.Column<int>(type: "int", nullable: false),
                    NewValue = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenSequenceAuditHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenSequenceAuditHistories_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScreenSequenceAuditHistories_FeatureFunctions_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "FeatureFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenSequences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ScreenId = table.Column<int>(type: "int", nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenSequences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenSequences_FeatureFunctions_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "FeatureFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScreenSequences_ScreenDefinition_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "ScreenDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScreenSequenceAuditHistories_ModifiedById",
                table: "ScreenSequenceAuditHistories",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenSequenceAuditHistories_ServiceId",
                table: "ScreenSequenceAuditHistories",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenSequences_ScreenId",
                table: "ScreenSequences",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenSequences_ServiceId",
                table: "ScreenSequences",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScreenSequenceAuditHistories");

            migrationBuilder.DropTable(
                name: "ScreenSequences");
        }
    }
}
