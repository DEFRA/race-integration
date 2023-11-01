using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportingDocuments_AspNetUsers_SuppliedById",
                table: "SupportingDocuments");

            migrationBuilder.DropTable(
                name: "ReservoirSupportingDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupportingDocuments",
                table: "SupportingDocuments");

            migrationBuilder.RenameTable(
                name: "SupportingDocuments",
                newName: "Document");

            migrationBuilder.RenameIndex(
                name: "IX_SupportingDocuments_SuppliedById",
                table: "Document",
                newName: "IX_Document_SuppliedById");

            migrationBuilder.AddColumn<string>(
                name: "RaceCertificateId",
                table: "FloodPlans",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Document",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentName",
                table: "Document",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<string>(
                name: "RaceDocumentId",
                table: "Document",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ReservoirId",
                table: "Document",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SuppliedViaServiceId",
                table: "Document",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsedTemplateEdition",
                table: "Document",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UsedTemplateVersion",
                table: "Document",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Document_ReservoirId",
                table: "Document",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_SuppliedViaServiceId",
                table: "Document",
                column: "SuppliedViaServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_AspNetUsers_SuppliedById",
                table: "Document",
                column: "SuppliedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_FeatureFunction_SuppliedViaServiceId",
                table: "Document",
                column: "SuppliedViaServiceId",
                principalTable: "FeatureFunction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Reservoirs_ReservoirId",
                table: "Document",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_AspNetUsers_SuppliedById",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_FeatureFunction_SuppliedViaServiceId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Reservoirs_ReservoirId",
                table: "Document");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_ReservoirId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_SuppliedViaServiceId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "RaceCertificateId",
                table: "FloodPlans");

            migrationBuilder.DropColumn(
                name: "RaceDocumentId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "ReservoirId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "SuppliedViaServiceId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "UsedTemplateEdition",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "UsedTemplateVersion",
                table: "Document");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "SupportingDocuments");

            migrationBuilder.RenameIndex(
                name: "IX_Document_SuppliedById",
                table: "SupportingDocuments",
                newName: "IX_SupportingDocuments_SuppliedById");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "SupportingDocuments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentName",
                table: "SupportingDocuments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupportingDocuments",
                table: "SupportingDocuments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ReservoirSupportingDocument",
                columns: table => new
                {
                    ReservoirId = table.Column<int>(type: "int", nullable: false),
                    SupportingDocumentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservoirSupportingDocument", x => new { x.ReservoirId, x.SupportingDocumentsId });
                    table.ForeignKey(
                        name: "FK_ReservoirSupportingDocument_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservoirSupportingDocument_SupportingDocuments_SupportingDocumentsId",
                        column: x => x.SupportingDocumentsId,
                        principalTable: "SupportingDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirSupportingDocument_SupportingDocumentsId",
                table: "ReservoirSupportingDocument",
                column: "SupportingDocumentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportingDocuments_AspNetUsers_SuppliedById",
                table: "SupportingDocuments",
                column: "SuppliedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
