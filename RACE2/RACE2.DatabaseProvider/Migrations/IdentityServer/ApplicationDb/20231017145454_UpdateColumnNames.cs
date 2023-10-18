using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Organisations_OrganisationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationAddresses_Addresses_Addressid",
                table: "OrganisationAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationAddresses_Organisations_OrganisationId",
                table: "OrganisationAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_Organisations_OrganisationId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Addresses_Addressid",
                table: "Reservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreenSequenceAuditHistories_FeatureFunctions_ServiceId",
                table: "ScreenSequenceAuditHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreenSequences_FeatureFunctions_ServiceId",
                table: "ScreenSequences");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionStatus_FeatureFunctions_ServiceId",
                table: "SubmissionStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Addresses_Addressid",
                table: "UserAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_AspNetUsers_UserDetailId",
                table: "UserAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_AspNetRoles_RoleId",
                table: "UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_FeatureFunctions_FeatureFunctionId",
                table: "UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organisations",
                table: "Organisations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganisationAddresses",
                table: "OrganisationAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeatureFunctions",
                table: "FeatureFunctions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "UserPermissions",
                newName: "Permission");

            migrationBuilder.RenameTable(
                name: "UserAddresses",
                newName: "UserAddress");

            migrationBuilder.RenameTable(
                name: "Organisations",
                newName: "Organisation");

            migrationBuilder.RenameTable(
                name: "OrganisationAddresses",
                newName: "OrganisationAddress");

            migrationBuilder.RenameTable(
                name: "FeatureFunctions",
                newName: "FeatureFunction");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "race_id",
                table: "AspNetUsers",
                newName: "cRaceId");

            migrationBuilder.RenameColumn(
                name: "c_type",
                table: "AspNetUsers",
                newName: "cType");

            migrationBuilder.RenameColumn(
                name: "c_status",
                table: "AspNetUsers",
                newName: "cStatus");

            migrationBuilder.RenameColumn(
                name: "c_saon",
                table: "AspNetUsers",
                newName: "cSaon");

            migrationBuilder.RenameColumn(
                name: "c_paon",
                table: "AspNetUsers",
                newName: "cPaon");

            migrationBuilder.RenameColumn(
                name: "c_mobile",
                table: "AspNetUsers",
                newName: "cMobile");

            migrationBuilder.RenameColumn(
                name: "c_last_name",
                table: "AspNetUsers",
                newName: "cLastName");

            migrationBuilder.RenameColumn(
                name: "c_last_access_date",
                table: "AspNetUsers",
                newName: "cLastAccessDate");

            migrationBuilder.RenameColumn(
                name: "c_job_title",
                table: "AspNetUsers",
                newName: "cJobTitle");

            migrationBuilder.RenameColumn(
                name: "c_first_name",
                table: "AspNetUsers",
                newName: "cFirstName");

            migrationBuilder.RenameColumn(
                name: "c_emergency_phone",
                table: "AspNetUsers",
                newName: "cEmergencyPhone");

            migrationBuilder.RenameColumn(
                name: "c_defra_id",
                table: "AspNetUsers",
                newName: "cDefraId");

            migrationBuilder.RenameColumn(
                name: "c_current_panel",
                table: "AspNetUsers",
                newName: "cCurrentPanel");

            migrationBuilder.RenameColumn(
                name: "c_created_on_date",
                table: "AspNetUsers",
                newName: "cCreatedOnDate");

            migrationBuilder.RenameColumn(
                name: "c_alternative_phone",
                table: "AspNetUsers",
                newName: "cAlternativePhone");

            migrationBuilder.RenameColumn(
                name: "c_alternative_mobile",
                table: "AspNetUsers",
                newName: "cAlternativeMobile");

            migrationBuilder.RenameColumn(
                name: "c_alternative_emergence_phone",
                table: "AspNetUsers",
                newName: "cAlternativeEmergencyPhone");

            migrationBuilder.RenameColumn(
                name: "c_alternative_email",
                table: "AspNetUsers",
                newName: "cAlternativeEmail");

            migrationBuilder.RenameColumn(
                name: "c_IsFirstTimeUser",
                table: "AspNetUsers",
                newName: "cIsFirstTimeUser");

            migrationBuilder.RenameColumn(
                name: "c_status",
                table: "AspNetUserRoles",
                newName: "cStatus");

            migrationBuilder.RenameColumn(
                name: "c_start_date",
                table: "AspNetUserRoles",
                newName: "cStartDate");

            migrationBuilder.RenameColumn(
                name: "c_end_date",
                table: "AspNetUserRoles",
                newName: "cEndDate");

            migrationBuilder.RenameColumn(
                name: "c_Id",
                table: "AspNetUserRoles",
                newName: "cId");

            migrationBuilder.RenameColumn(
                name: "c_parent_roleid",
                table: "AspNetRoles",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "c_display_name",
                table: "AspNetRoles",
                newName: "DisplayName");

            migrationBuilder.RenameColumn(
                name: "c_description",
                table: "AspNetRoles",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Start_date",
                table: "Permission",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "End_date",
                table: "Permission",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "Access_level",
                table: "Permission",
                newName: "AccessLevel");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_RoleId",
                table: "Permission",
                newName: "IX_Permission_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_FeatureFunctionId",
                table: "Permission",
                newName: "IX_Permission_FeatureFunctionId");

            migrationBuilder.RenameColumn(
                name: "AddressType",
                table: "UserAddress",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddresses_UserDetailId",
                table: "UserAddress",
                newName: "IX_UserAddress_UserDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddresses_Addressid",
                table: "UserAddress",
                newName: "IX_UserAddress_Addressid");

            migrationBuilder.RenameColumn(
                name: "BusinessType",
                table: "Organisation",
                newName: "cBusinessType");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationAddresses_OrganisationId",
                table: "OrganisationAddress",
                newName: "IX_OrganisationAddress_OrganisationId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationAddresses_Addressid",
                table: "OrganisationAddress",
                newName: "IX_OrganisationAddress_Addressid");

            migrationBuilder.RenameColumn(
                name: "Start_date",
                table: "FeatureFunction",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "End_date",
                table: "FeatureFunction",
                newName: "EndDate");

            migrationBuilder.AddColumn<string>(
                name: "cDisplayName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cFullName",
                table: "AspNetUsers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "UserDetailId",
                table: "UserAddress",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Addressid",
                table: "UserAddress",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrgName",
                table: "Organisation",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cRaceOganisationId",
                table: "Organisation",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine1",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permission",
                table: "Permission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddress",
                table: "UserAddress",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organisation",
                table: "Organisation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganisationAddress",
                table: "OrganisationAddress",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeatureFunction",
                table: "FeatureFunction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Organisation_OrganisationId",
                table: "AspNetUsers",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id");

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
                name: "FK_Permission_AspNetRoles_RoleId",
                table: "Permission",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_FeatureFunction_FeatureFunctionId",
                table: "Permission",
                column: "FeatureFunctionId",
                principalTable: "FeatureFunction",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Address_Addressid",
                table: "Reservoirs",
                column: "Addressid",
                principalTable: "Address",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenSequenceAuditHistories_FeatureFunction_ServiceId",
                table: "ScreenSequenceAuditHistories",
                column: "ServiceId",
                principalTable: "FeatureFunction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenSequences_FeatureFunction_ServiceId",
                table: "ScreenSequences",
                column: "ServiceId",
                principalTable: "FeatureFunction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionStatus_FeatureFunction_ServiceId",
                table: "SubmissionStatus",
                column: "ServiceId",
                principalTable: "FeatureFunction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_Address_Addressid",
                table: "UserAddress",
                column: "Addressid",
                principalTable: "Address",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_AspNetUsers_UserDetailId",
                table: "UserAddress",
                column: "UserDetailId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Organisation_OrganisationId",
                table: "AspNetUsers");

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
                name: "FK_Permission_AspNetRoles_RoleId",
                table: "Permission");

            migrationBuilder.DropForeignKey(
                name: "FK_Permission_FeatureFunction_FeatureFunctionId",
                table: "Permission");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Address_Addressid",
                table: "Reservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreenSequenceAuditHistories_FeatureFunction_ServiceId",
                table: "ScreenSequenceAuditHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreenSequences_FeatureFunction_ServiceId",
                table: "ScreenSequences");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionStatus_FeatureFunction_ServiceId",
                table: "SubmissionStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_Address_Addressid",
                table: "UserAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_AspNetUsers_UserDetailId",
                table: "UserAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddress",
                table: "UserAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permission",
                table: "Permission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganisationAddress",
                table: "OrganisationAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organisation",
                table: "Organisation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeatureFunction",
                table: "FeatureFunction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "cDisplayName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "cFullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "cRaceOganisationId",
                table: "Organisation");

            migrationBuilder.RenameTable(
                name: "UserAddress",
                newName: "UserAddresses");

            migrationBuilder.RenameTable(
                name: "Permission",
                newName: "UserPermissions");

            migrationBuilder.RenameTable(
                name: "OrganisationAddress",
                newName: "OrganisationAddresses");

            migrationBuilder.RenameTable(
                name: "Organisation",
                newName: "Organisations");

            migrationBuilder.RenameTable(
                name: "FeatureFunction",
                newName: "FeatureFunctions");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameColumn(
                name: "cType",
                table: "AspNetUsers",
                newName: "c_type");

            migrationBuilder.RenameColumn(
                name: "cStatus",
                table: "AspNetUsers",
                newName: "c_status");

            migrationBuilder.RenameColumn(
                name: "cSaon",
                table: "AspNetUsers",
                newName: "c_saon");

            migrationBuilder.RenameColumn(
                name: "cRaceId",
                table: "AspNetUsers",
                newName: "race_id");

            migrationBuilder.RenameColumn(
                name: "cPaon",
                table: "AspNetUsers",
                newName: "c_paon");

            migrationBuilder.RenameColumn(
                name: "cMobile",
                table: "AspNetUsers",
                newName: "c_mobile");

            migrationBuilder.RenameColumn(
                name: "cLastName",
                table: "AspNetUsers",
                newName: "c_last_name");

            migrationBuilder.RenameColumn(
                name: "cLastAccessDate",
                table: "AspNetUsers",
                newName: "c_last_access_date");

            migrationBuilder.RenameColumn(
                name: "cJobTitle",
                table: "AspNetUsers",
                newName: "c_job_title");

            migrationBuilder.RenameColumn(
                name: "cIsFirstTimeUser",
                table: "AspNetUsers",
                newName: "c_IsFirstTimeUser");

            migrationBuilder.RenameColumn(
                name: "cFirstName",
                table: "AspNetUsers",
                newName: "c_first_name");

            migrationBuilder.RenameColumn(
                name: "cEmergencyPhone",
                table: "AspNetUsers",
                newName: "c_emergency_phone");

            migrationBuilder.RenameColumn(
                name: "cDefraId",
                table: "AspNetUsers",
                newName: "c_defra_id");

            migrationBuilder.RenameColumn(
                name: "cCurrentPanel",
                table: "AspNetUsers",
                newName: "c_current_panel");

            migrationBuilder.RenameColumn(
                name: "cCreatedOnDate",
                table: "AspNetUsers",
                newName: "c_created_on_date");

            migrationBuilder.RenameColumn(
                name: "cAlternativePhone",
                table: "AspNetUsers",
                newName: "c_alternative_phone");

            migrationBuilder.RenameColumn(
                name: "cAlternativeMobile",
                table: "AspNetUsers",
                newName: "c_alternative_mobile");

            migrationBuilder.RenameColumn(
                name: "cAlternativeEmergencyPhone",
                table: "AspNetUsers",
                newName: "c_alternative_emergence_phone");

            migrationBuilder.RenameColumn(
                name: "cAlternativeEmail",
                table: "AspNetUsers",
                newName: "c_alternative_email");

            migrationBuilder.RenameColumn(
                name: "cStatus",
                table: "AspNetUserRoles",
                newName: "c_status");

            migrationBuilder.RenameColumn(
                name: "cStartDate",
                table: "AspNetUserRoles",
                newName: "c_start_date");

            migrationBuilder.RenameColumn(
                name: "cEndDate",
                table: "AspNetUserRoles",
                newName: "c_end_date");

            migrationBuilder.RenameColumn(
                name: "cId",
                table: "AspNetUserRoles",
                newName: "c_Id");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "AspNetRoles",
                newName: "c_parent_roleid");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "AspNetRoles",
                newName: "c_display_name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "AspNetRoles",
                newName: "c_description");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "UserAddresses",
                newName: "AddressType");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddress_UserDetailId",
                table: "UserAddresses",
                newName: "IX_UserAddresses_UserDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddress_Addressid",
                table: "UserAddresses",
                newName: "IX_UserAddresses_Addressid");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "UserPermissions",
                newName: "Start_date");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "UserPermissions",
                newName: "End_date");

            migrationBuilder.RenameColumn(
                name: "AccessLevel",
                table: "UserPermissions",
                newName: "Access_level");

            migrationBuilder.RenameIndex(
                name: "IX_Permission_RoleId",
                table: "UserPermissions",
                newName: "IX_UserPermissions_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Permission_FeatureFunctionId",
                table: "UserPermissions",
                newName: "IX_UserPermissions_FeatureFunctionId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationAddress_OrganisationId",
                table: "OrganisationAddresses",
                newName: "IX_OrganisationAddresses_OrganisationId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationAddress_Addressid",
                table: "OrganisationAddresses",
                newName: "IX_OrganisationAddresses_Addressid");

            migrationBuilder.RenameColumn(
                name: "cBusinessType",
                table: "Organisations",
                newName: "BusinessType");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "FeatureFunctions",
                newName: "Start_date");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "FeatureFunctions",
                newName: "End_date");

            migrationBuilder.AlterColumn<int>(
                name: "UserDetailId",
                table: "UserAddresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Addressid",
                table: "UserAddresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "OrgName",
                table: "Organisations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine1",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganisationAddresses",
                table: "OrganisationAddresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organisations",
                table: "Organisations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeatureFunctions",
                table: "FeatureFunctions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Organisations_OrganisationId",
                table: "AspNetUsers",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationAddresses_Addresses_Addressid",
                table: "OrganisationAddresses",
                column: "Addressid",
                principalTable: "Addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationAddresses_Organisations_OrganisationId",
                table: "OrganisationAddresses",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_Organisations_OrganisationId",
                table: "OrganisationReservoirs",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Addresses_Addressid",
                table: "Reservoirs",
                column: "Addressid",
                principalTable: "Addresses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenSequenceAuditHistories_FeatureFunctions_ServiceId",
                table: "ScreenSequenceAuditHistories",
                column: "ServiceId",
                principalTable: "FeatureFunctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenSequences_FeatureFunctions_ServiceId",
                table: "ScreenSequences",
                column: "ServiceId",
                principalTable: "FeatureFunctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionStatus_FeatureFunctions_ServiceId",
                table: "SubmissionStatus",
                column: "ServiceId",
                principalTable: "FeatureFunctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Addresses_Addressid",
                table: "UserAddresses",
                column: "Addressid",
                principalTable: "Addresses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_AspNetUsers_UserDetailId",
                table: "UserAddresses",
                column: "UserDetailId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_AspNetRoles_RoleId",
                table: "UserPermissions",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_FeatureFunctions_FeatureFunctionId",
                table: "UserPermissions",
                column: "FeatureFunctionId",
                principalTable: "FeatureFunctions",
                principalColumn: "Id");
        }
    }
}
