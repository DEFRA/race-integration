using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateSubmissionReferenceChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplianceSummary_Reservoirs_ReservoirId",
                table: "ComplianceSummary");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentEngineer_AspNetUsers_EngineerUserId",
                table: "DocumentEngineer");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentEngineer_Document_DocumentId",
                table: "DocumentEngineer");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentReservoir_Document_DocumentId",
                table: "DocumentReservoir");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentReservoir_Reservoirs_ReservoirId",
                table: "DocumentReservoir");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentSubmission_Document_DocumentId",
                table: "DocumentSubmission");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentSubmission_SubmissionStatus_SubmissionId",
                table: "DocumentSubmission");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationAddress_Address_Addressid",
                table: "OrganisationAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationAddress_Organisation_OrganisationId",
                table: "OrganisationAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_Organisation_OrganisationId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_Reservoirs_ReservoirId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_SafetyMeasures_Reservoirs_ReservoirId",
                table: "SafetyMeasures");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionStatus_ScreenDefinition_LastModifiedScreenId",
                table: "SubmissionStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReservoir_AspNetUsers_UserId",
                table: "UserReservoir");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReservoir_Reservoirs_ReservoirId",
                table: "UserReservoir");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionStatus_LastModifiedScreenId",
                table: "SubmissionStatus");

            migrationBuilder.RenameColumn(
                name: "LastModifiedScreenId",
                table: "SubmissionStatus",
                newName: "LastModifiedScreen");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserReservoir",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "UserReservoir",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SubmissionReference",
                table: "SubmissionStatus",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "SafetyMeasures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "OrganisationReservoirs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrganisationId",
                table: "OrganisationReservoirs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OrganisationAddress",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<int>(
                name: "OrganisationId",
                table: "OrganisationAddress",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Addressid",
                table: "OrganisationAddress",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MessageTemplate",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LogEvent",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Exception",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            //migrationBuilder.AddColumn<string>(
            //    name: "IP",
            //    table: "Logs",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "UserName",
            //    table: "Logs",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RevisionType",
                table: "FloodPlans",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "RevisionDetails",
                table: "FloodPlans",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<string>(
                name: "ReasonSummary",
                table: "EarlyInspection",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<int>(
                name: "SubmissionId",
                table: "DocumentSubmission",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "DocumentSubmission",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "DocumentReservoir",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "DocumentReservoir",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EngineerUserId",
                table: "DocumentEngineer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "DocumentEngineer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RaceDocumentId",
                table: "Document",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "ProtectiveMarking",
                table: "Document",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "FileType",
                table: "Document",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "FileLocation",
                table: "Document",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentStatus",
                table: "Document",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentDescription",
                table: "Document",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentAuthorName",
                table: "Document",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "ComplianceSummary",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Actions",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "RaceActionId",
                table: "Actions",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Priority",
                table: "Actions",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Actions",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplianceSummary_Reservoirs_ReservoirId",
                table: "ComplianceSummary",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentEngineer_AspNetUsers_EngineerUserId",
                table: "DocumentEngineer",
                column: "EngineerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentEngineer_Document_DocumentId",
                table: "DocumentEngineer",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentReservoir_Document_DocumentId",
                table: "DocumentReservoir",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentReservoir_Reservoirs_ReservoirId",
                table: "DocumentReservoir",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentSubmission_Document_DocumentId",
                table: "DocumentSubmission",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentSubmission_SubmissionStatus_SubmissionId",
                table: "DocumentSubmission",
                column: "SubmissionId",
                principalTable: "SubmissionStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationAddress_Address_Addressid",
                table: "OrganisationAddress",
                column: "Addressid",
                principalTable: "Address",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationAddress_Organisation_OrganisationId",
                table: "OrganisationAddress",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_Organisation_OrganisationId",
                table: "OrganisationReservoirs",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_Reservoirs_ReservoirId",
                table: "OrganisationReservoirs",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SafetyMeasures_Reservoirs_ReservoirId",
                table: "SafetyMeasures",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReservoir_AspNetUsers_UserId",
                table: "UserReservoir",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReservoir_Reservoirs_ReservoirId",
                table: "UserReservoir",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplianceSummary_Reservoirs_ReservoirId",
                table: "ComplianceSummary");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentEngineer_AspNetUsers_EngineerUserId",
                table: "DocumentEngineer");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentEngineer_Document_DocumentId",
                table: "DocumentEngineer");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentReservoir_Document_DocumentId",
                table: "DocumentReservoir");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentReservoir_Reservoirs_ReservoirId",
                table: "DocumentReservoir");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentSubmission_Document_DocumentId",
                table: "DocumentSubmission");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentSubmission_SubmissionStatus_SubmissionId",
                table: "DocumentSubmission");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationAddress_Address_Addressid",
                table: "OrganisationAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationAddress_Organisation_OrganisationId",
                table: "OrganisationAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_Organisation_OrganisationId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_Reservoirs_ReservoirId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_SafetyMeasures_Reservoirs_ReservoirId",
                table: "SafetyMeasures");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReservoir_AspNetUsers_UserId",
                table: "UserReservoir");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReservoir_Reservoirs_ReservoirId",
                table: "UserReservoir");

            migrationBuilder.DropColumn(
                name: "IP",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "LastModifiedScreen",
                table: "SubmissionStatus",
                newName: "LastModifiedScreenId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserReservoir",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "UserReservoir",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubmissionReference",
                table: "SubmissionStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "SafetyMeasures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "OrganisationReservoirs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganisationId",
                table: "OrganisationReservoirs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OrganisationAddress",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganisationId",
                table: "OrganisationAddress",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Addressid",
                table: "OrganisationAddress",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MessageTemplate",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LogEvent",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Exception",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RevisionType",
                table: "FloodPlans",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RevisionDetails",
                table: "FloodPlans",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReasonSummary",
                table: "EarlyInspection",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubmissionId",
                table: "DocumentSubmission",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "DocumentSubmission",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "DocumentReservoir",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "DocumentReservoir",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EngineerUserId",
                table: "DocumentEngineer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "DocumentEngineer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RaceDocumentId",
                table: "Document",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProtectiveMarking",
                table: "Document",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileType",
                table: "Document",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileLocation",
                table: "Document",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentStatus",
                table: "Document",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentDescription",
                table: "Document",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentAuthorName",
                table: "Document",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservoirId",
                table: "ComplianceSummary",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Actions",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RaceActionId",
                table: "Actions",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Priority",
                table: "Actions",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Actions",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionStatus_LastModifiedScreenId",
                table: "SubmissionStatus",
                column: "LastModifiedScreenId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplianceSummary_Reservoirs_ReservoirId",
                table: "ComplianceSummary",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentEngineer_AspNetUsers_EngineerUserId",
                table: "DocumentEngineer",
                column: "EngineerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentEngineer_Document_DocumentId",
                table: "DocumentEngineer",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentReservoir_Document_DocumentId",
                table: "DocumentReservoir",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentReservoir_Reservoirs_ReservoirId",
                table: "DocumentReservoir",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentSubmission_Document_DocumentId",
                table: "DocumentSubmission",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentSubmission_SubmissionStatus_SubmissionId",
                table: "DocumentSubmission",
                column: "SubmissionId",
                principalTable: "SubmissionStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationAddress_Address_Addressid",
                table: "OrganisationAddress",
                column: "Addressid",
                principalTable: "Address",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationAddress_Organisation_OrganisationId",
                table: "OrganisationAddress",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_Organisation_OrganisationId",
                table: "OrganisationReservoirs",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_Reservoirs_ReservoirId",
                table: "OrganisationReservoirs",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SafetyMeasures_Reservoirs_ReservoirId",
                table: "SafetyMeasures",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionStatus_ScreenDefinition_LastModifiedScreenId",
                table: "SubmissionStatus",
                column: "LastModifiedScreenId",
                principalTable: "ScreenDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReservoir_AspNetUsers_UserId",
                table: "UserReservoir",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReservoir_Reservoirs_ReservoirId",
                table: "UserReservoir",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
