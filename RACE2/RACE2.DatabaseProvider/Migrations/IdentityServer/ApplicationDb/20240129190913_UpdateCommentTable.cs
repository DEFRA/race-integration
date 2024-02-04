using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateCommentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SourceSubmissionId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SourceSubmissionId",
                table: "Comments",
                column: "SourceSubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_SubmissionStatus_SourceSubmissionId",
                table: "Comments",
                column: "SourceSubmissionId",
                principalTable: "SubmissionStatus",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_SubmissionStatus_SourceSubmissionId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SourceSubmissionId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SourceSubmissionId",
                table: "Comments");
        }
    }
}
