using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileShopAPI.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productImg");

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "image",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_image_ProductId",
                table: "image",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "fk_images_product",
                table: "image",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_images_product",
                table: "image");

            migrationBuilder.DropIndex(
                name: "IX_image_ProductId",
                table: "image");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "image");

            migrationBuilder.CreateTable(
                name: "productImg",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imageId = table.Column<long>(type: "bigint", nullable: false),
                    productId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productImg", x => x.id);
                    table.ForeignKey(
                        name: "fk_productImg_image",
                        column: x => x.imageId,
                        principalTable: "image",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_productImg_product",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productImg_imageId",
                table: "productImg",
                column: "imageId");

            migrationBuilder.CreateIndex(
                name: "IX_productImg_productId",
                table: "productImg",
                column: "productId");
        }
    }
}
