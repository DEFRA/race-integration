using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddColumnSubmissionTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<string>(
                name: "RevisionSummary",
                table: "SubmissionStatus",
                type: "nvarchar(1024)",
                nullable: true);
                //oldClrType: typeof(string),
                //oldType: "nvarchar(1024)",
                //oldMaxLength: 1024,
                //oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "RevisionSummary",
            //    table: "SubmissionStatus",
            //    type: "nvarchar(1024)",
            //    maxLength: 1024,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)",
            //    oldNullable: true);

        }
    }
}
