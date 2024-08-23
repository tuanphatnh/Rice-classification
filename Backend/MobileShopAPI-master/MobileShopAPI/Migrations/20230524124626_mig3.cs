using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileShopAPI.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "url",
                table: "image",
                type: "nvarchar(max)",
                nullable: true,
                comment: "url hình ảnh",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "url hình ảnh");

            migrationBuilder.AlterColumn<bool>(
                name: "isCover",
                table: "image",
                type: "bit",
                nullable: true,
                defaultValueSql: "((0))",
                comment: "hình ảnh là ảnh bìa",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValueSql: "((0))",
                oldComment: "hình ảnh là ảnh bìa");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "avatar_url",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar_url",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "url",
                table: "image",
                type: "bigint",
                nullable: true,
                comment: "url hình ảnh",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "url hình ảnh");

            migrationBuilder.AlterColumn<int>(
                name: "isCover",
                table: "image",
                type: "int",
                nullable: true,
                defaultValueSql: "((0))",
                comment: "hình ảnh là ảnh bìa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((0))",
                oldComment: "hình ảnh là ảnh bìa");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValueSql: "((0))");
        }
    }
}
