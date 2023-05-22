using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateDataModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_AspNetUsers_OwnedById",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Addresses_addressid",
                table: "Reservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservoirSupportingDocument_SupportingDocuments_DocumentsId",
                table: "ReservoirSupportingDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservoirSupportingDocument",
                table: "ReservoirSupportingDocument");

            migrationBuilder.DropIndex(
                name: "IX_ReservoirSupportingDocument_ReservoirId",
                table: "ReservoirSupportingDocument");

            migrationBuilder.DropIndex(
                name: "IX_Actions_OwnedById",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "OwnedById",
                table: "Actions");

            migrationBuilder.RenameColumn(
                name: "Appointment_type",
                table: "UserReservoirs",
                newName: "AppointmentType");

            migrationBuilder.RenameColumn(
                name: "Appointment_start_date",
                table: "UserReservoirs",
                newName: "AppointmentStartDate");

            migrationBuilder.RenameColumn(
                name: "Appointment_end_date",
                table: "UserReservoirs",
                newName: "AppointmentEndDate");

            migrationBuilder.RenameColumn(
                name: "DocumentsId",
                table: "ReservoirSupportingDocument",
                newName: "SupportingDocumentsId");

            migrationBuilder.RenameColumn(
                name: "capacity",
                table: "Reservoirs",
                newName: "Capacity");

            migrationBuilder.RenameColumn(
                name: "addressid",
                table: "Reservoirs",
                newName: "Addressid");

            migrationBuilder.RenameColumn(
                name: "verified_details_date",
                table: "Reservoirs",
                newName: "VerifiedDetailsDate");

            migrationBuilder.RenameColumn(
                name: "top_water_level",
                table: "Reservoirs",
                newName: "TopWaterLevel");

            migrationBuilder.RenameColumn(
                name: "surface_area",
                table: "Reservoirs",
                newName: "SurfaceArea");

            migrationBuilder.RenameColumn(
                name: "registered_name",
                table: "Reservoirs",
                newName: "RegisteredName");

            migrationBuilder.RenameColumn(
                name: "registered_category",
                table: "Reservoirs",
                newName: "RegisteredCategory");

            migrationBuilder.RenameColumn(
                name: "reference_number",
                table: "Reservoirs",
                newName: "ReferenceNumber");

            migrationBuilder.RenameColumn(
                name: "race_reservoir_id",
                table: "Reservoirs",
                newName: "RaceReservoirId");

            migrationBuilder.RenameColumn(
                name: "public_name",
                table: "Reservoirs",
                newName: "PublicName");

            migrationBuilder.RenameColumn(
                name: "public_category",
                table: "Reservoirs",
                newName: "PublicCategory");

            migrationBuilder.RenameColumn(
                name: "next_inspection_date",
                table: "Reservoirs",
                newName: "NextInspectionDate");

            migrationBuilder.RenameColumn(
                name: "last_inspection_date",
                table: "Reservoirs",
                newName: "LastInspectionDate");

            migrationBuilder.RenameColumn(
                name: "key_facts",
                table: "Reservoirs",
                newName: "KeyFacts");

            migrationBuilder.RenameColumn(
                name: "has_multiple_dams",
                table: "Reservoirs",
                newName: "HasMultipleDams");

            migrationBuilder.RenameColumn(
                name: "grid_reference",
                table: "Reservoirs",
                newName: "GridReference");

            migrationBuilder.RenameColumn(
                name: "construction_start_date",
                table: "Reservoirs",
                newName: "ConstructionStartDate");

            migrationBuilder.RenameIndex(
                name: "IX_Reservoirs_addressid",
                table: "Reservoirs",
                newName: "IX_Reservoirs_Addressid");

            migrationBuilder.RenameColumn(
                name: "OwnerType",
                table: "Actions",
                newName: "RaceActionId");

            migrationBuilder.AddColumn<string>(
                name: "RaceAppointmentId",
                table: "UserReservoirs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                table: "SafetyMeasures",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Othertype",
                table: "SafetyMeasures",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "SafetyMeasures",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AddColumn<string>(
                name: "RaceSafetyMeasureId",
                table: "SafetyMeasures",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OperatorType",
                table: "Reservoirs",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessType",
                table: "Organisations",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RaceEarlyInspectionId",
                table: "EarlyInspections",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RelatesToObject",
                table: "Comments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<string>(
                name: "RaceCommentId",
                table: "Comments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "c_alternative_email",
                table: "AspNetUsers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "c_alternative_emergence_phone",
                table: "AspNetUsers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "c_alternative_mobile",
                table: "AspNetUsers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "c_alternative_phone",
                table: "AspNetUsers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "race_id",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RaceAddressKey",
                table: "Addresses",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservoirSupportingDocument",
                table: "ReservoirSupportingDocument",
                columns: new[] { "ReservoirId", "SupportingDocumentsId" });

            migrationBuilder.CreateTable(
                name: "OrganisationReservoirs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganisationId = table.Column<int>(type: "int", nullable: false),
                    ReservoirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationReservoirs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganisationReservoirs_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationReservoirs_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirSupportingDocument_SupportingDocumentsId",
                table: "ReservoirSupportingDocument",
                column: "SupportingDocumentsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationReservoirs_OrganisationId",
                table: "OrganisationReservoirs",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationReservoirs_ReservoirId",
                table: "OrganisationReservoirs",
                column: "ReservoirId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Addresses_Addressid",
                table: "Reservoirs",
                column: "Addressid",
                principalTable: "Addresses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservoirSupportingDocument_SupportingDocuments_SupportingDocumentsId",
                table: "ReservoirSupportingDocument",
                column: "SupportingDocumentsId",
                principalTable: "SupportingDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Addresses_Addressid",
                table: "Reservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservoirSupportingDocument_SupportingDocuments_SupportingDocumentsId",
                table: "ReservoirSupportingDocument");

            migrationBuilder.DropTable(
                name: "OrganisationReservoirs");

            migrationBuilder.DropTable(
                name: "SubmissionStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservoirSupportingDocument",
                table: "ReservoirSupportingDocument");

            migrationBuilder.DropIndex(
                name: "IX_ReservoirSupportingDocument_SupportingDocumentsId",
                table: "ReservoirSupportingDocument");

            migrationBuilder.DropColumn(
                name: "RaceAppointmentId",
                table: "UserReservoirs");

            migrationBuilder.DropColumn(
                name: "RaceSafetyMeasureId",
                table: "SafetyMeasures");

            migrationBuilder.DropColumn(
                name: "OperatorType",
                table: "Reservoirs");

            migrationBuilder.DropColumn(
                name: "BusinessType",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "RaceEarlyInspectionId",
                table: "EarlyInspections");

            migrationBuilder.DropColumn(
                name: "RaceCommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "c_alternative_email",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "c_alternative_emergence_phone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "c_alternative_mobile",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "c_alternative_phone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "race_id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RaceAddressKey",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "AppointmentType",
                table: "UserReservoirs",
                newName: "Appointment_type");

            migrationBuilder.RenameColumn(
                name: "AppointmentStartDate",
                table: "UserReservoirs",
                newName: "Appointment_start_date");

            migrationBuilder.RenameColumn(
                name: "AppointmentEndDate",
                table: "UserReservoirs",
                newName: "Appointment_end_date");

            migrationBuilder.RenameColumn(
                name: "SupportingDocumentsId",
                table: "ReservoirSupportingDocument",
                newName: "DocumentsId");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Reservoirs",
                newName: "capacity");

            migrationBuilder.RenameColumn(
                name: "Addressid",
                table: "Reservoirs",
                newName: "addressid");

            migrationBuilder.RenameColumn(
                name: "VerifiedDetailsDate",
                table: "Reservoirs",
                newName: "verified_details_date");

            migrationBuilder.RenameColumn(
                name: "TopWaterLevel",
                table: "Reservoirs",
                newName: "top_water_level");

            migrationBuilder.RenameColumn(
                name: "SurfaceArea",
                table: "Reservoirs",
                newName: "surface_area");

            migrationBuilder.RenameColumn(
                name: "RegisteredName",
                table: "Reservoirs",
                newName: "registered_name");

            migrationBuilder.RenameColumn(
                name: "RegisteredCategory",
                table: "Reservoirs",
                newName: "registered_category");

            migrationBuilder.RenameColumn(
                name: "ReferenceNumber",
                table: "Reservoirs",
                newName: "reference_number");

            migrationBuilder.RenameColumn(
                name: "RaceReservoirId",
                table: "Reservoirs",
                newName: "race_reservoir_id");

            migrationBuilder.RenameColumn(
                name: "PublicName",
                table: "Reservoirs",
                newName: "public_name");

            migrationBuilder.RenameColumn(
                name: "PublicCategory",
                table: "Reservoirs",
                newName: "public_category");

            migrationBuilder.RenameColumn(
                name: "NextInspectionDate",
                table: "Reservoirs",
                newName: "next_inspection_date");

            migrationBuilder.RenameColumn(
                name: "LastInspectionDate",
                table: "Reservoirs",
                newName: "last_inspection_date");

            migrationBuilder.RenameColumn(
                name: "KeyFacts",
                table: "Reservoirs",
                newName: "key_facts");

            migrationBuilder.RenameColumn(
                name: "HasMultipleDams",
                table: "Reservoirs",
                newName: "has_multiple_dams");

            migrationBuilder.RenameColumn(
                name: "GridReference",
                table: "Reservoirs",
                newName: "grid_reference");

            migrationBuilder.RenameColumn(
                name: "ConstructionStartDate",
                table: "Reservoirs",
                newName: "construction_start_date");

            migrationBuilder.RenameIndex(
                name: "IX_Reservoirs_Addressid",
                table: "Reservoirs",
                newName: "IX_Reservoirs_addressid");

            migrationBuilder.RenameColumn(
                name: "RaceActionId",
                table: "Actions",
                newName: "OwnerType");

            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                table: "SafetyMeasures",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Othertype",
                table: "SafetyMeasures",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "SafetyMeasures",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RelatesToObject",
                table: "Comments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnedById",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservoirSupportingDocument",
                table: "ReservoirSupportingDocument",
                columns: new[] { "DocumentsId", "ReservoirId" });

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirSupportingDocument_ReservoirId",
                table: "ReservoirSupportingDocument",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_OwnedById",
                table: "Actions",
                column: "OwnedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_AspNetUsers_OwnedById",
                table: "Actions",
                column: "OwnedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Addresses_addressid",
                table: "Reservoirs",
                column: "addressid",
                principalTable: "Addresses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservoirSupportingDocument_SupportingDocuments_DocumentsId",
                table: "ReservoirSupportingDocument",
                column: "DocumentsId",
                principalTable: "SupportingDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
