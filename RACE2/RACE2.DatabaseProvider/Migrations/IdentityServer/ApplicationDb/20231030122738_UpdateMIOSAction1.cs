using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateMIOSAction1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplianceSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ComplianceIndicatorType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ComplianceIndicatorOtherDesc = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ComplianceStatus = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceSubmissionId = table.Column<int>(type: "int", nullable: false),
                    ReservoirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplianceSummary_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComplianceSummary_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FloodPlanTesting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    PlanElement = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    TestDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    TestInterval = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloodPlanTesting", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplianceSummary_CreatedById",
                table: "ComplianceSummary",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ComplianceSummary_ReservoirId",
                table: "ComplianceSummary",
                column: "ReservoirId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplianceSummary");

            migrationBuilder.DropTable(
                name: "FloodPlanTesting");
        }
    }
}
