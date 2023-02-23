﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_display_name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    c_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    c_parent_id = table.Column<int>(type: "int", nullable: false),
                    c_start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    c_end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_defra_id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_type = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: " "),
                    c_display_name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, defaultValue: " "),
                    c_first_name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: " "),
                    c_last_name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: " "),
                    c_mobile = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_emergency_phone = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_organisation_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    c_organisation_name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_job_title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_current_panel = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_paon = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_saon = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_status = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: " "),
                    c_created_on_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    c_last_access_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    c_password = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true, defaultValue: " "),
                    c_password_hint = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, defaultValue: " "),
                    c_password_retry_count = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureFunctions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    display_name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    description = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    default_value = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureFunctions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservoirs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    race_reservoir_id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    public_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    registered_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    reference_number = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    public_category = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    registered_category = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    grid_reference = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    nearest_postcode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    nearest_town = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    surface_area = table.Column<int>(type: "int", nullable: false),
                    top_water_level = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    has_multiple_dams = table.Column<bool>(type: "bit", nullable: false),
                    key_facts = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    construction_start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    verified_details_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    last_inspection_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    next_inspection_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    addressid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservoirs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reservoirs_Addresses_addressid",
                        column: x => x.addressid,
                        principalTable: "Addresses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    c_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    c_start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    c_end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => x.c_Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUserDetail",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    UserDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUserDetail", x => new { x.RolesId, x.UserDetailId });
                    table.ForeignKey(
                        name: "FK_RoleUserDetail_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUserDetail_AspNetUsers_UserDetailId",
                        column: x => x.UserDetailId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    feature_function_id = table.Column<int>(type: "int", nullable: false),
                    access_level = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FeatureFunctionId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserPermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPermissions_FeatureFunctions_FeatureFunctionId",
                        column: x => x.FeatureFunctionId,
                        principalTable: "FeatureFunctions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReservoirUserDetail",
                columns: table => new
                {
                    Reservoirsid = table.Column<int>(type: "int", nullable: false),
                    usersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservoirUserDetail", x => new { x.Reservoirsid, x.usersId });
                    table.ForeignKey(
                        name: "FK_ReservoirUserDetail_AspNetUsers_usersId",
                        column: x => x.usersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservoirUserDetail_Reservoirs_Reservoirsid",
                        column: x => x.Reservoirsid,
                        principalTable: "Reservoirs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_addressid",
                table: "Reservoirs",
                column: "addressid");

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirUserDetail_usersId",
                table: "ReservoirUserDetail",
                column: "usersId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUserDetail_UserDetailId",
                table: "RoleUserDetail",
                column: "UserDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_FeatureFunctionId",
                table: "UserPermissions",
                column: "FeatureFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_RoleId",
                table: "UserPermissions",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ReservoirUserDetail");

            migrationBuilder.DropTable(
                name: "RoleUserDetail");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Reservoirs");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "FeatureFunctions");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
