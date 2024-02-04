using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateChangeHistoryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionsChangeHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservoirId = table.Column<int>(type: "int", nullable: false),
                    SourceSubmissionId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ChangeDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsBackEndChange = table.Column<bool>(type: "bit", nullable: false),
                    ChangeByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionsChangeHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionsChangeHistory_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionsChangeHistory_AspNetUsers_ChangeByUserId",
                        column: x => x.ChangeByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionsChangeHistory_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ActionsChangeHistory_SubmissionStatus_SourceSubmissionId",
                        column: x => x.SourceSubmissionId,
                        principalTable: "SubmissionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CommentsChangeHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservoirId = table.Column<int>(type: "int", nullable: false),
                    SourceSubmissionId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ChangeDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsBackEndChange = table.Column<bool>(type: "bit", nullable: false),
                    ChangeByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsChangeHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentsChangeHistory_AspNetUsers_ChangeByUserId",
                        column: x => x.ChangeByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentsChangeHistory_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CommentsChangeHistory_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentsChangeHistory_SubmissionStatus_SourceSubmissionId",
                        column: x => x.SourceSubmissionId,
                        principalTable: "SubmissionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SafetyMeasuresChangeHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservoirId = table.Column<int>(type: "int", nullable: false),
                    SourceSubmissionId = table.Column<int>(type: "int", nullable: false),
                    MeasureId = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ChangeDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsBackEndChange = table.Column<bool>(type: "bit", nullable: false),
                    ChangeByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyMeasuresChangeHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyMeasuresChangeHistory_AspNetUsers_ChangeByUserId",
                        column: x => x.ChangeByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SafetyMeasuresChangeHistory_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SafetyMeasuresChangeHistory_SafetyMeasures_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "SafetyMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SafetyMeasuresChangeHistory_SubmissionStatus_SourceSubmissionId",
                        column: x => x.SourceSubmissionId,
                        principalTable: "SubmissionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionsChangeHistory_ActionId",
                table: "ActionsChangeHistory",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionsChangeHistory_ChangeByUserId",
                table: "ActionsChangeHistory",
                column: "ChangeByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionsChangeHistory_ReservoirId",
                table: "ActionsChangeHistory",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionsChangeHistory_SourceSubmissionId",
                table: "ActionsChangeHistory",
                column: "SourceSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsChangeHistory_ChangeByUserId",
                table: "CommentsChangeHistory",
                column: "ChangeByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsChangeHistory_CommentId",
                table: "CommentsChangeHistory",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsChangeHistory_ReservoirId",
                table: "CommentsChangeHistory",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsChangeHistory_SourceSubmissionId",
                table: "CommentsChangeHistory",
                column: "SourceSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyMeasuresChangeHistory_ChangeByUserId",
                table: "SafetyMeasuresChangeHistory",
                column: "ChangeByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyMeasuresChangeHistory_MeasureId",
                table: "SafetyMeasuresChangeHistory",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyMeasuresChangeHistory_ReservoirId",
                table: "SafetyMeasuresChangeHistory",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyMeasuresChangeHistory_SourceSubmissionId",
                table: "SafetyMeasuresChangeHistory",
                column: "SourceSubmissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionsChangeHistory");

            migrationBuilder.DropTable(
                name: "CommentsChangeHistory");

            migrationBuilder.DropTable(
                name: "SafetyMeasuresChangeHistory");
        }
    }
}
