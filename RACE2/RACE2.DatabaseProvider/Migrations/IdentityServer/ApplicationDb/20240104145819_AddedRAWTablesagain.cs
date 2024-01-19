using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class AddedRAWTablesagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RAW_ActionSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ReservoirName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Mandatory = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    LastModifiedDateTime = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAW_ActionSummary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RAW_MaintenanceMeasures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ReservoirName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    LastModifiedDateTime = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAW_MaintenanceMeasures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RAW_MIOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ReservoirName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Outstanding = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Deadline = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    LastModifiedDateTime = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAW_MIOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RAW_StatementDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ReservoirName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    SupervisingEngineer_short = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsTypeOfStatement12_2 = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsTypeOfStatement12_2A = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    NearestTown = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    PeriodStart = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    PeriodEnd = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    StatementDate = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    GridReference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UndertakeName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UndertakerAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UndertakerEmail = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UndertakerPhone = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    SupervisingEngineer_long = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    SupervisingEngineerCompany = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    SupervisingEngineerAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    SupervisingEngineerEmail = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    SupervisingEngineerPhone = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    HasAlternativeEngineerYes = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    HasAlternativeEngineerNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    LastInspectingEngineerName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    LastInspectingEngineerPhone = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    LastInspectionDate = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    LastCertificationDate = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsEarlyInspectionRequiredYes = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsEarlyInspectionRequiredNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    NextInspectionDate = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    HasNoMaintenanceItems = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    HasNoMIOSItems = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    HasNoWatchItems = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    LastModifiedDateTime = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAW_StatementDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RAW_WatchItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ReservoirName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    LastModifiedDateTime = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAW_WatchItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RAW_ActionSummary");

            migrationBuilder.DropTable(
                name: "RAW_MaintenanceMeasures");

            migrationBuilder.DropTable(
                name: "RAW_MIOS");

            migrationBuilder.DropTable(
                name: "RAW_StatementDetails");

            migrationBuilder.DropTable(
                name: "RAW_WatchItems");
        }
    }
}
