using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdatedReservoirHistoryTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservoirDetailsChangeHistory_SubmissionStatus_SourceSubmissionId",
                table: "ReservoirDetailsChangeHistory");

            migrationBuilder.AlterColumn<int>(
                name: "SourceSubmissionId",
                table: "ReservoirDetailsChangeHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservoirDetailsChangeHistory_SubmissionStatus_SourceSubmissionId",
                table: "ReservoirDetailsChangeHistory",
                column: "SourceSubmissionId",
                principalTable: "SubmissionStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservoirDetailsChangeHistory_SubmissionStatus_SourceSubmissionId",
                table: "ReservoirDetailsChangeHistory");

            migrationBuilder.AlterColumn<int>(
                name: "SourceSubmissionId",
                table: "ReservoirDetailsChangeHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservoirDetailsChangeHistory_SubmissionStatus_SourceSubmissionId",
                table: "ReservoirDetailsChangeHistory",
                column: "SourceSubmissionId",
                principalTable: "SubmissionStatus",
                principalColumn: "Id");
        }
    }
}
