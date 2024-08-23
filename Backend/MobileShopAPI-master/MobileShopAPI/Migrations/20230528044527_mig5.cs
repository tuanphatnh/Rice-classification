using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileShopAPI.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "size",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HexValue",
                table: "color",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RgbValue",
                table: "color",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "size");

            migrationBuilder.DropColumn(
                name: "HexValue",
                table: "color");

            migrationBuilder.DropColumn(
                name: "RgbValue",
                table: "color");
        }
    }
}
