using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddDocumentTemplateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TemplateName = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    TemplateVersion = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    TemplateStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TemplateEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservoirId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrganisationId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    IsDefaultTemplate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTemplate_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentTemplate_FeatureFunction_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "FeatureFunction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentTemplate_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentTemplate_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplate_OrganisationId",
                table: "DocumentTemplate",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplate_ReservoirId",
                table: "DocumentTemplate",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplate_ServiceId",
                table: "DocumentTemplate",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplate_UserId",
                table: "DocumentTemplate",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentTemplate");
        }
    }
}
