using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    c_parent_roleid = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureFunctions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureFunctions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservoirs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    race_reservoir_id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    public_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    registered_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    reference_number = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    public_category = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    registered_category = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    grid_reference = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    surface_area = table.Column<int>(type: "int", nullable: false),
                    top_water_level = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    has_multiple_dams = table.Column<bool>(type: "bit", nullable: false),
                    key_facts = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    construction_start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    verified_details_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    last_inspection_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    next_inspection_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    addressid = table.Column<int>(type: "int", nullable: true),
                    NearestTown = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservoirs", x => x.Id);
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
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Access_level = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Start_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    End_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FeatureFunctionId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_defra_id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_parent_userid = table.Column<int>(type: "int", nullable: false),
                    c_type = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: " "),
                    c_first_name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: " "),
                    c_last_name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: " "),
                    c_mobile = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_emergency_phone = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_job_title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_current_panel = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_paon = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_saon = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, defaultValue: " "),
                    c_status = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: " "),
                    c_created_on_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    c_last_access_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    c_IsFirstTimeUser = table.Column<bool>(type: "bit", nullable: false),
                    OrganisationId = table.Column<int>(type: "int", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganisationAddresses",
                columns: table => new
                {
                    Addressesid = table.Column<int>(type: "int", nullable: false),
                    OrganisationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationAddresses", x => new { x.Addressesid, x.OrganisationId });
                    table.ForeignKey(
                        name: "FK_OrganisationAddresses_Addresses_Addressesid",
                        column: x => x.Addressesid,
                        principalTable: "Addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationAddresses_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EarlyInspections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ReasonType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ReasonSummary = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ReservoirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarlyInspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EarlyInspections_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FloodPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsTested = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    RequiresRevision = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    RevisionType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    RevisionDetails = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ReservoirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloodPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FloodPlans_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SafetyMeasures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Othertype = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Createddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Targetdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ReservoirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyMeasures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyMeasures_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IsComplianceAction = table.Column<bool>(type: "bit", nullable: false),
                    OwnerType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    OwnedById = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ReservoirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actions_AspNetUsers_OwnedById",
                        column: x => x.OwnedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actions_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
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
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelatesToObject = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    RelatesToRecord = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ParentCommentidId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ClosedById = table.Column<int>(type: "int", nullable: true),
                    ClosedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_ClosedById",
                        column: x => x.ClosedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentCommentidId",
                        column: x => x.ParentCommentidId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupportingDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    FileLocation = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DocumentDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentStatus = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DocumentAuthorName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ProtectiveMarking = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DateSent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuppliedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportingDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportingDocuments_AspNetUsers_SuppliedById",
                        column: x => x.SuppliedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserDetailId = table.Column<int>(type: "int", nullable: true),
                    Addressid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddresses_Addresses_Addressid",
                        column: x => x.Addressid,
                        principalTable: "Addresses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_UserAddresses_AspNetUsers_UserDetailId",
                        column: x => x.UserDetailId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserReservoirs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserDetailId = table.Column<int>(type: "int", nullable: false),
                    ReservoirId = table.Column<int>(type: "int", nullable: false),
                    Appointment_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Appointment_start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Appointment_end_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReservoirs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReservoirs_AspNetUsers_UserDetailId",
                        column: x => x.UserDetailId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserReservoirs_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservoirSupportingDocument",
                columns: table => new
                {
                    DocumentsId = table.Column<int>(type: "int", nullable: false),
                    ReservoirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservoirSupportingDocument", x => new { x.DocumentsId, x.ReservoirId });
                    table.ForeignKey(
                        name: "FK_ReservoirSupportingDocument_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservoirSupportingDocument_SupportingDocuments_DocumentsId",
                        column: x => x.DocumentsId,
                        principalTable: "SupportingDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actions_OwnedById",
                table: "Actions",
                column: "OwnedById");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ReservoirId",
                table: "Actions",
                column: "ReservoirId");

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
                name: "IX_AspNetUsers_OrganisationId",
                table: "AspNetUsers",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ClosedById",
                table: "Comments",
                column: "ClosedById");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedById",
                table: "Comments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentidId",
                table: "Comments",
                column: "ParentCommentidId");

            migrationBuilder.CreateIndex(
                name: "IX_EarlyInspections_ReservoirId",
                table: "EarlyInspections",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_FloodPlans_ReservoirId",
                table: "FloodPlans",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationAddresses_OrganisationId",
                table: "OrganisationAddresses",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_addressid",
                table: "Reservoirs",
                column: "addressid");

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirSupportingDocument_ReservoirId",
                table: "ReservoirSupportingDocument",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyMeasures_ReservoirId",
                table: "SafetyMeasures",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportingDocuments_SuppliedById",
                table: "SupportingDocuments",
                column: "SuppliedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_Addressid",
                table: "UserAddresses",
                column: "Addressid");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserDetailId",
                table: "UserAddresses",
                column: "UserDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_FeatureFunctionId",
                table: "UserPermissions",
                column: "FeatureFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_RoleId",
                table: "UserPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReservoirs_ReservoirId",
                table: "UserReservoirs",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReservoirs_UserDetailId",
                table: "UserReservoirs",
                column: "UserDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actions");

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
                name: "Comments");

            migrationBuilder.DropTable(
                name: "EarlyInspections");

            migrationBuilder.DropTable(
                name: "FloodPlans");

            migrationBuilder.DropTable(
                name: "OrganisationAddresses");

            migrationBuilder.DropTable(
                name: "ReservoirSupportingDocument");

            migrationBuilder.DropTable(
                name: "SafetyMeasures");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "UserReservoirs");

            migrationBuilder.DropTable(
                name: "SupportingDocuments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "FeatureFunctions");

            migrationBuilder.DropTable(
                name: "Reservoirs");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Organisations");
        }
    }
}
