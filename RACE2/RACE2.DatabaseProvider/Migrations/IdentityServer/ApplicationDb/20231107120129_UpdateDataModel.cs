using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RACE2.DatabaseProvider.Migrations.IdentityServer.ApplicationDb
{
    public partial class UpdateDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Address_Addressid",
                table: "Reservoirs");

            migrationBuilder.DropIndex(
                name: "IX_Reservoirs_Addressid",
                table: "Reservoirs");

            migrationBuilder.DropColumn(
                name: "Addressid",
                table: "Reservoirs");

            migrationBuilder.AlterColumn<decimal>(
                name: "TopWaterLevel",
                table: "Reservoirs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "SurfaceArea",
                table: "Reservoirs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RegisteredName",
                table: "Reservoirs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasMultipleDams",
                table: "Reservoirs",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Reservoirs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TopWaterLevel",
                table: "Reservoirs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SurfaceArea",
                table: "Reservoirs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RegisteredName",
                table: "Reservoirs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<bool>(
                name: "HasMultipleDams",
                table: "Reservoirs",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Reservoirs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Addressid",
                table: "Reservoirs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_Addressid",
                table: "Reservoirs",
                column: "Addressid");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Address_Addressid",
                table: "Reservoirs",
                column: "Addressid",
                principalTable: "Address",
                principalColumn: "id");
        }
    }
}
