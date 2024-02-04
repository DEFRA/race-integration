using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateDBReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_ClosedById",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_CreatedById",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplianceSummary_AspNetUsers_CreatedById",
                table: "ComplianceSummary");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_AspNetUsers_SuppliedById",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_FeatureFunction_SuppliedViaServiceId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_FloodPlans_Reservoirs_ReservoirId",
                table: "FloodPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_PrimaryContactUserId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_SecondaryContactUserId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_Organisation_OrganisationId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_Reservoirs_ReservoirId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreenDefinition_AspNetUsers_ModifiedById",
                table: "ScreenDefinition");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreenSequenceAuditHistories_AspNetUsers_ModifiedById",
                table: "ScreenSequenceAuditHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreenSequenceAuditHistories_FeatureFunction_ServiceId",
                table: "ScreenSequenceAuditHistories");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_SubmissionStatus_AspNetUsers_LastModifiedById",
            //    table: "SubmissionStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionStatus_AspNetUsers_SubmittedById",
                table: "SubmissionStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScreenSequenceAuditHistories",
                table: "ScreenSequenceAuditHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganisationReservoirs",
                table: "OrganisationReservoirs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FloodPlans",
                table: "FloodPlans");

            migrationBuilder.DropColumn(
                name: "BackendValue",
                table: "PicklistMappings");

            migrationBuilder.DropColumn(
                name: "PicklistValue",
                table: "PicklistMappings");

            migrationBuilder.DropColumn(
                name: "cBranch",
                table: "Organisation");

            migrationBuilder.DropColumn(
                name: "SourceSubmissionId",
                table: "ComplianceSummary");

            migrationBuilder.RenameTable(
                name: "ScreenSequenceAuditHistories",
                newName: "ScreenSequenceAuditHistory");

            migrationBuilder.RenameTable(
                name: "OrganisationReservoirs",
                newName: "OrganisationReservoir");

            migrationBuilder.RenameTable(
                name: "FloodPlans",
                newName: "FloodPlan");

            migrationBuilder.RenameColumn(
                name: "RaceAppointmentId",
                table: "UserReservoir",
                newName: "BackEndAppointmentId");

            migrationBuilder.RenameColumn(
                name: "SubmittedById",
                table: "SubmissionStatus",
                newName: "SubmittedByUserId");

            migrationBuilder.RenameColumn(
                name: "OverrideUsedTemplate",
                table: "SubmissionStatus",
                newName: "OverrideTemplateName");

            migrationBuilder.RenameColumn(
                name: "LastModifiedScreen",
                table: "SubmissionStatus",
                newName: "LastModifiedScreenId");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "SubmissionStatus",
                newName: "LastModifiedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SubmissionStatus_SubmittedById",
                table: "SubmissionStatus",
                newName: "IX_SubmissionStatus_SubmittedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SubmissionStatus_LastModifiedById",
                table: "SubmissionStatus",
                newName: "IX_SubmissionStatus_LastModifiedByUserId");

            migrationBuilder.RenameColumn(
                name: "ScreenName",
                table: "ScreenDefinition",
                newName: "Reference");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "ScreenDefinition",
                newName: "LastModifiedByUserId");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "ScreenDefinition",
                newName: "LastModifiedDateTime");

            migrationBuilder.RenameIndex(
                name: "IX_ScreenDefinition_ModifiedById",
                table: "ScreenDefinition",
                newName: "IX_ScreenDefinition_LastModifiedByUserId");

            migrationBuilder.RenameColumn(
                name: "RaceSafetyMeasureId",
                table: "SafetyMeasures",
                newName: "BackEndSafetyMeasureId");

            migrationBuilder.RenameColumn(
                name: "RaceReservoirId",
                table: "Reservoirs",
                newName: "BackEndReservoirId");

            migrationBuilder.RenameColumn(
                name: "cRaceOganisationId",
                table: "Organisation",
                newName: "cBackEndOganisationId");

            migrationBuilder.RenameColumn(
                name: "RaceEarlyInspectionId",
                table: "EarlyInspection",
                newName: "BackEndEarlyInspectionId");

            migrationBuilder.RenameColumn(
                name: "UsedTemplateVersion",
                table: "Document",
                newName: "TemplateVersion");

            migrationBuilder.RenameColumn(
                name: "UsedTemplateEdition",
                table: "Document",
                newName: "UploadFileType");

            migrationBuilder.RenameColumn(
                name: "SuppliedViaServiceId",
                table: "Document",
                newName: "UploadByUserId");

            migrationBuilder.RenameColumn(
                name: "SuppliedById",
                table: "Document",
                newName: "SourceServiceId");

            migrationBuilder.RenameColumn(
                name: "FileType",
                table: "Document",
                newName: "TemplateType");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Document",
                newName: "UploadFileName");

            migrationBuilder.RenameColumn(
                name: "FileLocation",
                table: "Document",
                newName: "UploadFileLocation");

            migrationBuilder.RenameColumn(
                name: "DocumentName",
                table: "Document",
                newName: "DocumentTitle");

            migrationBuilder.RenameIndex(
                name: "IX_Document_SuppliedViaServiceId",
                table: "Document",
                newName: "IX_Document_UploadByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Document_SuppliedById",
                table: "Document",
                newName: "IX_Document_SourceServiceId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "ComplianceSummary",
                newName: "SourceSubmissionIdId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplianceSummary_CreatedById",
                table: "ComplianceSummary",
                newName: "IX_ComplianceSummary_SourceSubmissionIdId");

            migrationBuilder.RenameColumn(
                name: "RelatesToRecord",
                table: "Comments",
                newName: "RelatesToRecordId");

            migrationBuilder.RenameColumn(
                name: "RaceCommentId",
                table: "Comments",
                newName: "BackEndCommentId");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Comments",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Comments",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "ClosedOn",
                table: "Comments",
                newName: "ClosedDate");

            migrationBuilder.RenameColumn(
                name: "ClosedById",
                table: "Comments",
                newName: "ClosedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CreatedById",
                table: "Comments",
                newName: "IX_Comments_CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ClosedById",
                table: "Comments",
                newName: "IX_Comments_ClosedByUserId");

            migrationBuilder.RenameColumn(
                name: "cRaceId",
                table: "AspNetUsers",
                newName: "cBackEndUserId");

            migrationBuilder.RenameColumn(
                name: "cCreatedOnDate",
                table: "AspNetUsers",
                newName: "cCreatedDate");

            migrationBuilder.RenameColumn(
                name: "RaceAddressKey",
                table: "Address",
                newName: "BackEndAddressKey");

            migrationBuilder.RenameColumn(
                name: "RaceActionId",
                table: "Actions",
                newName: "BackEndActionId");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "ScreenSequenceAuditHistory",
                newName: "LastModifiedDateTime");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "ScreenSequenceAuditHistory",
                newName: "LastModifiedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ScreenSequenceAuditHistories_ServiceId",
                table: "ScreenSequenceAuditHistory",
                newName: "IX_ScreenSequenceAuditHistory_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ScreenSequenceAuditHistories_ModifiedById",
                table: "ScreenSequenceAuditHistory",
                newName: "IX_ScreenSequenceAuditHistory_LastModifiedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationReservoirs_SecondaryContactUserId",
                table: "OrganisationReservoir",
                newName: "IX_OrganisationReservoir_SecondaryContactUserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationReservoirs_ReservoirId",
                table: "OrganisationReservoir",
                newName: "IX_OrganisationReservoir_ReservoirId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationReservoirs_PrimaryContactUserId",
                table: "OrganisationReservoir",
                newName: "IX_OrganisationReservoir_PrimaryContactUserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationReservoirs_OrganisationId",
                table: "OrganisationReservoir",
                newName: "IX_OrganisationReservoir_OrganisationId");

            migrationBuilder.RenameColumn(
                name: "RaceCertificateId",
                table: "FloodPlan",
                newName: "BackEndCertificateId");

            migrationBuilder.RenameIndex(
                name: "IX_FloodPlans_ReservoirId",
                table: "FloodPlan",
                newName: "IX_FloodPlan_ReservoirId");

            migrationBuilder.AddColumn<string>(
                name: "BackEndValueId",
                table: "PicklistMappings",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "PicklistMappings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PicklistValueIdId",
                table: "PicklistMappings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "PicklistMappings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "ComplianceSummary",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ComplianceSummary",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cLastModifiedByUserIdId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "cLastModifiedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnedByName",
                table: "Actions",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnedByUserId",
                table: "Actions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerRolecId",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScreenSequenceAuditHistory",
                table: "ScreenSequenceAuditHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganisationReservoir",
                table: "OrganisationReservoir",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FloodPlan",
                table: "FloodPlan",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PanelMembership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PanelName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    MembershipNumber = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanelMembership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PanelMembership_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PicklistMappings_PicklistValueIdId",
                table: "PicklistMappings",
                column: "PicklistValueIdId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplianceSummary_CreatedByUserId",
                table: "ComplianceSummary",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_cLastModifiedByUserIdId",
                table: "AspNetUsers",
                column: "cLastModifiedByUserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_OwnedByUserId",
                table: "Actions",
                column: "OwnedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_OwnerRolecId",
                table: "Actions",
                column: "OwnerRolecId");

            migrationBuilder.CreateIndex(
                name: "IX_PanelMembership_UserId",
                table: "PanelMembership",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_AspNetUserRoles_OwnerRolecId",
                table: "Actions",
                column: "OwnerRolecId",
                principalTable: "AspNetUserRoles",
                principalColumn: "cId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_AspNetUsers_OwnedByUserId",
                table: "Actions",
                column: "OwnedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_cLastModifiedByUserIdId",
                table: "AspNetUsers",
                column: "cLastModifiedByUserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_ClosedByUserId",
                table: "Comments",
                column: "ClosedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_CreatedByUserId",
                table: "Comments",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplianceSummary_AspNetUsers_CreatedByUserId",
                table: "ComplianceSummary",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplianceSummary_SubmissionStatus_SourceSubmissionIdId",
                table: "ComplianceSummary",
                column: "SourceSubmissionIdId",
                principalTable: "SubmissionStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_AspNetUsers_UploadByUserId",
                table: "Document",
                column: "UploadByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Document_FeatureFunction_SourceServiceId",
            //    table: "Document",
            //    column: "SourceServiceId",
            //    principalTable: "FeatureFunction",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.NoAction
            //    );

            migrationBuilder.AddForeignKey(
                name: "FK_FloodPlan_Reservoirs_ReservoirId",
                table: "FloodPlan",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoir_AspNetUsers_PrimaryContactUserId",
                table: "OrganisationReservoir",
                column: "PrimaryContactUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoir_AspNetUsers_SecondaryContactUserId",
                table: "OrganisationReservoir",
                column: "SecondaryContactUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoir_Organisation_OrganisationId",
                table: "OrganisationReservoir",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoir_Reservoirs_ReservoirId",
                table: "OrganisationReservoir",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PicklistMappings_PicklistDefinitions_PicklistValueIdId",
                table: "PicklistMappings",
                column: "PicklistValueIdId",
                principalTable: "PicklistDefinitions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenDefinition_AspNetUsers_LastModifiedByUserId",
                table: "ScreenDefinition",
                column: "LastModifiedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenSequenceAuditHistory_AspNetUsers_LastModifiedByUserId",
                table: "ScreenSequenceAuditHistory",
                column: "LastModifiedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenSequenceAuditHistory_FeatureFunction_ServiceId",
                table: "ScreenSequenceAuditHistory",
                column: "ServiceId",
                principalTable: "FeatureFunction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SubmissionStatus_AspNetUsers_LastModifiedByUserId",
            //    table: "SubmissionStatus",
            //    column: "LastModifiedByUserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionStatus_AspNetUsers_SubmittedByUserId",
                table: "SubmissionStatus",
                column: "SubmittedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_AspNetUserRoles_OwnerRolecId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_AspNetUsers_OwnedByUserId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_cLastModifiedByUserIdId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_ClosedByUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_CreatedByUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplianceSummary_AspNetUsers_CreatedByUserId",
                table: "ComplianceSummary");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplianceSummary_SubmissionStatus_SourceSubmissionIdId",
                table: "ComplianceSummary");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_AspNetUsers_UploadByUserId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_FeatureFunction_SourceServiceId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_FloodPlan_Reservoirs_ReservoirId",
                table: "FloodPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoir_AspNetUsers_PrimaryContactUserId",
                table: "OrganisationReservoir");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoir_AspNetUsers_SecondaryContactUserId",
                table: "OrganisationReservoir");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoir_Organisation_OrganisationId",
                table: "OrganisationReservoir");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoir_Reservoirs_ReservoirId",
                table: "OrganisationReservoir");

            migrationBuilder.DropForeignKey(
                name: "FK_PicklistMappings_PicklistDefinitions_PicklistValueIdId",
                table: "PicklistMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreenDefinition_AspNetUsers_LastModifiedByUserId",
                table: "ScreenDefinition");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreenSequenceAuditHistory_AspNetUsers_LastModifiedByUserId",
                table: "ScreenSequenceAuditHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreenSequenceAuditHistory_FeatureFunction_ServiceId",
                table: "ScreenSequenceAuditHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionStatus_AspNetUsers_LastModifiedByUserId",
                table: "SubmissionStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionStatus_AspNetUsers_SubmittedByUserId",
                table: "SubmissionStatus");

            migrationBuilder.DropTable(
                name: "PanelMembership");

            migrationBuilder.DropIndex(
                name: "IX_PicklistMappings_PicklistValueIdId",
                table: "PicklistMappings");

            migrationBuilder.DropIndex(
                name: "IX_ComplianceSummary_CreatedByUserId",
                table: "ComplianceSummary");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_cLastModifiedByUserIdId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Actions_OwnedByUserId",
                table: "Actions");

            migrationBuilder.DropIndex(
                name: "IX_Actions_OwnerRolecId",
                table: "Actions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScreenSequenceAuditHistory",
                table: "ScreenSequenceAuditHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganisationReservoir",
                table: "OrganisationReservoir");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FloodPlan",
                table: "FloodPlan");

            migrationBuilder.DropColumn(
                name: "BackEndValueId",
                table: "PicklistMappings");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "PicklistMappings");

            migrationBuilder.DropColumn(
                name: "PicklistValueIdId",
                table: "PicklistMappings");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "PicklistMappings");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "ComplianceSummary");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ComplianceSummary");

            migrationBuilder.DropColumn(
                name: "cLastModifiedByUserIdId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "cLastModifiedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OwnedByName",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "OwnedByUserId",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "OwnerRolecId",
                table: "Actions");

            migrationBuilder.RenameTable(
                name: "ScreenSequenceAuditHistory",
                newName: "ScreenSequenceAuditHistories");

            migrationBuilder.RenameTable(
                name: "OrganisationReservoir",
                newName: "OrganisationReservoirs");

            migrationBuilder.RenameTable(
                name: "FloodPlan",
                newName: "FloodPlans");

            migrationBuilder.RenameColumn(
                name: "BackEndAppointmentId",
                table: "UserReservoir",
                newName: "RaceAppointmentId");

            migrationBuilder.RenameColumn(
                name: "SubmittedByUserId",
                table: "SubmissionStatus",
                newName: "SubmittedById");

            migrationBuilder.RenameColumn(
                name: "OverrideTemplateName",
                table: "SubmissionStatus",
                newName: "OverrideUsedTemplate");

            migrationBuilder.RenameColumn(
                name: "LastModifiedScreenId",
                table: "SubmissionStatus",
                newName: "LastModifiedScreen");

            migrationBuilder.RenameColumn(
                name: "LastModifiedByUserId",
                table: "SubmissionStatus",
                newName: "LastModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_SubmissionStatus_SubmittedByUserId",
                table: "SubmissionStatus",
                newName: "IX_SubmissionStatus_SubmittedById");

            migrationBuilder.RenameIndex(
                name: "IX_SubmissionStatus_LastModifiedByUserId",
                table: "SubmissionStatus",
                newName: "IX_SubmissionStatus_LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "ScreenDefinition",
                newName: "ScreenName");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDateTime",
                table: "ScreenDefinition",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "LastModifiedByUserId",
                table: "ScreenDefinition",
                newName: "ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_ScreenDefinition_LastModifiedByUserId",
                table: "ScreenDefinition",
                newName: "IX_ScreenDefinition_ModifiedById");

            migrationBuilder.RenameColumn(
                name: "BackEndSafetyMeasureId",
                table: "SafetyMeasures",
                newName: "RaceSafetyMeasureId");

            migrationBuilder.RenameColumn(
                name: "BackEndReservoirId",
                table: "Reservoirs",
                newName: "RaceReservoirId");

            migrationBuilder.RenameColumn(
                name: "cBackEndOganisationId",
                table: "Organisation",
                newName: "cRaceOganisationId");

            migrationBuilder.RenameColumn(
                name: "BackEndEarlyInspectionId",
                table: "EarlyInspection",
                newName: "RaceEarlyInspectionId");

            migrationBuilder.RenameColumn(
                name: "UploadFileType",
                table: "Document",
                newName: "UsedTemplateEdition");

            migrationBuilder.RenameColumn(
                name: "UploadFileName",
                table: "Document",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "UploadFileLocation",
                table: "Document",
                newName: "FileLocation");

            migrationBuilder.RenameColumn(
                name: "UploadByUserId",
                table: "Document",
                newName: "SuppliedViaServiceId");

            migrationBuilder.RenameColumn(
                name: "TemplateVersion",
                table: "Document",
                newName: "UsedTemplateVersion");

            migrationBuilder.RenameColumn(
                name: "TemplateType",
                table: "Document",
                newName: "FileType");

            migrationBuilder.RenameColumn(
                name: "SourceServiceId",
                table: "Document",
                newName: "SuppliedById");

            migrationBuilder.RenameColumn(
                name: "DocumentTitle",
                table: "Document",
                newName: "DocumentName");

            migrationBuilder.RenameIndex(
                name: "IX_Document_UploadByUserId",
                table: "Document",
                newName: "IX_Document_SuppliedViaServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Document_SourceServiceId",
                table: "Document",
                newName: "IX_Document_SuppliedById");

            migrationBuilder.RenameColumn(
                name: "SourceSubmissionIdId",
                table: "ComplianceSummary",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_ComplianceSummary_SourceSubmissionIdId",
                table: "ComplianceSummary",
                newName: "IX_ComplianceSummary_CreatedById");

            migrationBuilder.RenameColumn(
                name: "RelatesToRecordId",
                table: "Comments",
                newName: "RelatesToRecord");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Comments",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Comments",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "ClosedDate",
                table: "Comments",
                newName: "ClosedOn");

            migrationBuilder.RenameColumn(
                name: "ClosedByUserId",
                table: "Comments",
                newName: "ClosedById");

            migrationBuilder.RenameColumn(
                name: "BackEndCommentId",
                table: "Comments",
                newName: "RaceCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CreatedByUserId",
                table: "Comments",
                newName: "IX_Comments_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ClosedByUserId",
                table: "Comments",
                newName: "IX_Comments_ClosedById");

            migrationBuilder.RenameColumn(
                name: "cCreatedDate",
                table: "AspNetUsers",
                newName: "cCreatedOnDate");

            migrationBuilder.RenameColumn(
                name: "cBackEndUserId",
                table: "AspNetUsers",
                newName: "cRaceId");

            migrationBuilder.RenameColumn(
                name: "BackEndAddressKey",
                table: "Address",
                newName: "RaceAddressKey");

            migrationBuilder.RenameColumn(
                name: "BackEndActionId",
                table: "Actions",
                newName: "RaceActionId");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDateTime",
                table: "ScreenSequenceAuditHistories",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "LastModifiedByUserId",
                table: "ScreenSequenceAuditHistories",
                newName: "ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_ScreenSequenceAuditHistory_ServiceId",
                table: "ScreenSequenceAuditHistories",
                newName: "IX_ScreenSequenceAuditHistories_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ScreenSequenceAuditHistory_LastModifiedByUserId",
                table: "ScreenSequenceAuditHistories",
                newName: "IX_ScreenSequenceAuditHistories_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationReservoir_SecondaryContactUserId",
                table: "OrganisationReservoirs",
                newName: "IX_OrganisationReservoirs_SecondaryContactUserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationReservoir_ReservoirId",
                table: "OrganisationReservoirs",
                newName: "IX_OrganisationReservoirs_ReservoirId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationReservoir_PrimaryContactUserId",
                table: "OrganisationReservoirs",
                newName: "IX_OrganisationReservoirs_PrimaryContactUserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationReservoir_OrganisationId",
                table: "OrganisationReservoirs",
                newName: "IX_OrganisationReservoirs_OrganisationId");

            migrationBuilder.RenameColumn(
                name: "BackEndCertificateId",
                table: "FloodPlans",
                newName: "RaceCertificateId");

            migrationBuilder.RenameIndex(
                name: "IX_FloodPlan_ReservoirId",
                table: "FloodPlans",
                newName: "IX_FloodPlans_ReservoirId");

            migrationBuilder.AddColumn<string>(
                name: "BackendValue",
                table: "PicklistMappings",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PicklistValue",
                table: "PicklistMappings",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cBranch",
                table: "Organisation",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceSubmissionId",
                table: "ComplianceSummary",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScreenSequenceAuditHistories",
                table: "ScreenSequenceAuditHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganisationReservoirs",
                table: "OrganisationReservoirs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FloodPlans",
                table: "FloodPlans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_ClosedById",
                table: "Comments",
                column: "ClosedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_CreatedById",
                table: "Comments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplianceSummary_AspNetUsers_CreatedById",
                table: "ComplianceSummary",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

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
                name: "FK_FloodPlans_Reservoirs_ReservoirId",
                table: "FloodPlans",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_PrimaryContactUserId",
                table: "OrganisationReservoirs",
                column: "PrimaryContactUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_SecondaryContactUserId",
                table: "OrganisationReservoirs",
                column: "SecondaryContactUserId",
                principalTable: "AspNetUsers",
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
                name: "FK_ScreenDefinition_AspNetUsers_ModifiedById",
                table: "ScreenDefinition",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenSequenceAuditHistories_AspNetUsers_ModifiedById",
                table: "ScreenSequenceAuditHistories",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenSequenceAuditHistories_FeatureFunction_ServiceId",
                table: "ScreenSequenceAuditHistories",
                column: "ServiceId",
                principalTable: "FeatureFunction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SubmissionStatus_AspNetUsers_LastModifiedById",
            //    table: "SubmissionStatus",
            //    column: "LastModifiedById",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionStatus_AspNetUsers_SubmittedById",
                table: "SubmissionStatus",
                column: "SubmittedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
