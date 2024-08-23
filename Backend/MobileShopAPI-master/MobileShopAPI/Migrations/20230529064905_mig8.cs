using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileShopAPI.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isCover",
                table: "image",
                type: "bit",
                nullable: false,
                defaultValueSql: "((0))",
                comment: "hình ảnh là ảnh bìa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((0))",
                oldComment: "hình ảnh là ảnh bìa");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "image",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "image");

            migrationBuilder.AlterColumn<bool>(
                name: "isCover",
                table: "image",
                type: "bit",
                nullable: true,
                defaultValueSql: "((0))",
                comment: "hình ảnh là ảnh bìa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((0))",
                oldComment: "hình ảnh là ảnh bìa");
        }
    }
}
