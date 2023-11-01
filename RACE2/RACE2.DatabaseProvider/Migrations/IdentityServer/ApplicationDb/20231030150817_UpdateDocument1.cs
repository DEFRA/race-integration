using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateDocument1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentEngineer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    EngineerUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentEngineer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentEngineer_AspNetUsers_EngineerUserId",
                        column: x => x.EngineerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentEngineer_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DocumentRelationship",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    RelatedDocumentId = table.Column<int>(type: "int", nullable: false),
                    RelationshipType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRelationship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentRelationship_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentRelationship_Document_RelatedDocumentId",
                        column: x => x.RelatedDocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DocumentReservoir",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    ReservoirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentReservoir", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentReservoir_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentReservoir_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentSubmission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    SubmissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSubmission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentSubmission_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentSubmission_SubmissionStatus_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "SubmissionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StatementDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    StatementType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PeriodStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextStatementDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatementDetails_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentEngineer_DocumentId",
                table: "DocumentEngineer",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentEngineer_EngineerUserId",
                table: "DocumentEngineer",
                column: "EngineerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRelationship_DocumentId",
                table: "DocumentRelationship",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRelationship_RelatedDocumentId",
                table: "DocumentRelationship",
                column: "RelatedDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentReservoir_DocumentId",
                table: "DocumentReservoir",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentReservoir_ReservoirId",
                table: "DocumentReservoir",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSubmission_DocumentId",
                table: "DocumentSubmission",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSubmission_SubmissionId",
                table: "DocumentSubmission",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementDetails_DocumentId",
                table: "StatementDetails",
                column: "DocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentEngineer");

            migrationBuilder.DropTable(
                name: "DocumentRelationship");

            migrationBuilder.DropTable(
                name: "DocumentReservoir");

            migrationBuilder.DropTable(
                name: "DocumentSubmission");

            migrationBuilder.DropTable(
                name: "StatementDetails");
        }
    }
}
