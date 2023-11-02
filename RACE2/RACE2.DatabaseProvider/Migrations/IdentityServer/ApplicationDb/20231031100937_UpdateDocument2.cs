using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateDocument2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_PrimaryContactId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_SecondaryContactId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionStatus_AspNetUsers_ModifiedById",
                table: "SubmissionStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_AspNetUsers_UserDetailId",
                table: "UserAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReservoir_AspNetUsers_UserDetailId",
                table: "UserReservoir");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionStatus_ModifiedById",
                table: "SubmissionStatus");

            migrationBuilder.RenameColumn(
                name: "UserDetailId",
                table: "UserReservoir",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReservoir_UserDetailId",
                table: "UserReservoir",
                newName: "IX_UserReservoir_UserId");

            migrationBuilder.RenameColumn(
                name: "UserDetailId",
                table: "UserAddress",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddress_UserDetailId",
                table: "UserAddress",
                newName: "IX_UserAddress_UserId");

            migrationBuilder.RenameColumn(
                name: "override_template",
                table: "SubmissionStatus",
                newName: "OverrideUsedTemplate");

            migrationBuilder.RenameColumn(
                name: "SubmittedOn",
                table: "SubmissionStatus",
                newName: "SubmittedDateTime");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "SubmissionStatus",
                newName: "SubmissionReference");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "SubmissionStatus",
                newName: "LastModifiedDateTime");

            migrationBuilder.RenameColumn(
                name: "SecondaryContactId",
                table: "OrganisationReservoirs",
                newName: "SecondaryContactUserId");

            migrationBuilder.RenameColumn(
                name: "PrimaryContactId",
                table: "OrganisationReservoirs",
                newName: "PrimaryContactUserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationReservoirs_SecondaryContactId",
                table: "OrganisationReservoirs",
                newName: "IX_OrganisationReservoirs_SecondaryContactUserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationReservoirs_PrimaryContactId",
                table: "OrganisationReservoirs",
                newName: "IX_OrganisationReservoirs_PrimaryContactUserId");

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedById",
                table: "SubmissionStatus",
                type: "int",
                nullable: true
                );

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionStatus_LastModifiedById",
                table: "SubmissionStatus",
                column: "LastModifiedById");

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
                name: "FK_SubmissionStatus_AspNetUsers_LastModifiedByUserId",
                table: "SubmissionStatus",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_AspNetUsers_UserId",
                table: "UserAddress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReservoir_AspNetUsers_UserId",
                table: "UserReservoir",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_PrimaryContactUserId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_SecondaryContactUserId",
                table: "OrganisationReservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionStatus_AspNetUsers_LastModifiedById",
                table: "SubmissionStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_AspNetUsers_UserId",
                table: "UserAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReservoir_AspNetUsers_UserId",
                table: "UserReservoir");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionStatus_LastModifiedById",
                table: "SubmissionStatus");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "SubmissionStatus");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserReservoir",
                newName: "UserDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReservoir_UserId",
                table: "UserReservoir",
                newName: "IX_UserReservoir_UserDetailId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserAddress",
                newName: "UserDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddress_UserId",
                table: "UserAddress",
                newName: "IX_UserAddress_UserDetailId");

            migrationBuilder.RenameColumn(
                name: "SubmittedDateTime",
                table: "SubmissionStatus",
                newName: "SubmittedOn");

            migrationBuilder.RenameColumn(
                name: "SubmissionReference",
                table: "SubmissionStatus",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "OverrideUsedTemplate",
                table: "SubmissionStatus",
                newName: "override_template");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDateTime",
                table: "SubmissionStatus",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "SecondaryContactUserId",
                table: "OrganisationReservoirs",
                newName: "SecondaryContactId");

            migrationBuilder.RenameColumn(
                name: "PrimaryContactUserId",
                table: "OrganisationReservoirs",
                newName: "PrimaryContactId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationReservoirs_SecondaryContactUserId",
                table: "OrganisationReservoirs",
                newName: "IX_OrganisationReservoirs_SecondaryContactId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationReservoirs_PrimaryContactUserId",
                table: "OrganisationReservoirs",
                newName: "IX_OrganisationReservoirs_PrimaryContactId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionStatus_ModifiedById",
                table: "SubmissionStatus",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_PrimaryContactId",
                table: "OrganisationReservoirs",
                column: "PrimaryContactId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationReservoirs_AspNetUsers_SecondaryContactId",
                table: "OrganisationReservoirs",
                column: "SecondaryContactId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionStatus_AspNetUsers_ModifiedById",
                table: "SubmissionStatus",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_AspNetUsers_UserDetailId",
                table: "UserAddress",
                column: "UserDetailId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReservoir_AspNetUsers_UserDetailId",
                table: "UserReservoir",
                column: "UserDetailId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
