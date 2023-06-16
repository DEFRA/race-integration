using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateDataModelforScreenNew1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubmittedBy",
                table: "SubmissionStatus",
                newName: "SubmittedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedScreen",
                table: "SubmissionStatus",
                newName: "LastModifiedScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionStatus_LastModifiedScreenId",
                table: "SubmissionStatus",
                column: "LastModifiedScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionStatus_SubmittedById",
                table: "SubmissionStatus",
                column: "SubmittedById");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionStatus_AspNetUsers_SubmittedById",
                table: "SubmissionStatus",
                column: "SubmittedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id"
                );

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionStatus_ScreenDefinition_LastModifiedScreenId",
                table: "SubmissionStatus",
                column: "LastModifiedScreenId",
                principalTable: "ScreenDefinition",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionStatus_AspNetUsers_SubmittedById",
                table: "SubmissionStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionStatus_ScreenDefinition_LastModifiedScreenId",
                table: "SubmissionStatus");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionStatus_LastModifiedScreenId",
                table: "SubmissionStatus");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionStatus_SubmittedById",
                table: "SubmissionStatus");

            migrationBuilder.RenameColumn(
                name: "SubmittedById",
                table: "SubmissionStatus",
                newName: "SubmittedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedScreenId",
                table: "SubmissionStatus",
                newName: "LastModifiedScreen");
        }
    }
}
