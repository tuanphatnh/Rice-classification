using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileShopAPI.Migrations
{
    public partial class mig20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "subscription_package",
                type: "int",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "report",
                type: "int",
                nullable: false,
                defaultValueSql: "((0))");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "coin_package",
                type: "int",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "coin_action",
                type: "int",
                nullable: false,
                defaultValueSql: "((1))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "subscription_package");

            migrationBuilder.DropColumn(
                name: "status",
                table: "report");

            migrationBuilder.DropColumn(
                name: "status",
                table: "coin_package");

            migrationBuilder.DropColumn(
                name: "status",
                table: "coin_action");
        }
    }
}
