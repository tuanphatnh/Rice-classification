using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileShopAPI.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "brand",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image_url = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "url hình ảnh"),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brand", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image_url = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "url hình ảnh"),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "color",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    colorName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_color", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<long>(type: "bigint", nullable: true, comment: "url hình ảnh"),
                    isCover = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))", comment: "hình ảnh là ảnh bìa"),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    total = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    userId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())"),
                    updateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_AspNetUsers",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "size",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sizeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_size", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    orderId = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    vnp_Version = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    vnp_Command = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    vnp_TmnCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    vnp_Amount = table.Column<long>(type: "bigint", nullable: true),
                    vnp_BankCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    vnp_CreateDate = table.Column<long>(type: "bigint", nullable: true),
                    vnp_CurrCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    vnp_IpAddr = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    vnp_Locale = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    vnp_OrderInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vnp_OrderType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    vnp_TxnRef = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    vnp_SecureHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_AspNetUsers",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_transaction_order",
                        column: x => x.orderId,
                        principalTable: "order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stock = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "int", nullable: true),
                    categoryId = table.Column<long>(type: "bigint", nullable: false),
                    brandId = table.Column<long>(type: "bigint", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())"),
                    updateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    userId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    sizeId = table.Column<long>(type: "bigint", nullable: false, comment: "part of primaryKey"),
                    colorId = table.Column<long>(type: "bigint", nullable: false, comment: "part of primaryKey")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_AspNetUsers",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_product_brand",
                        column: x => x.brandId,
                        principalTable: "brand",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_product_catagory",
                        column: x => x.categoryId,
                        principalTable: "category",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_product_color",
                        column: x => x.colorId,
                        principalTable: "color",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_product_size",
                        column: x => x.sizeId,
                        principalTable: "size",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "orderDetail",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<long>(type: "bigint", nullable: false),
                    orderId = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    totalPrice = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderDetail", x => x.id);
                    table.ForeignKey(
                        name: "fk_orderDetail_product",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orderDetail_UserOrder",
                        column: x => x.orderId,
                        principalTable: "order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "productImg",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<long>(type: "bigint", nullable: false),
                    imageId = table.Column<long>(type: "bigint", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "user_rating",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rating = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((1))", comment: "1,2,3,4,5"),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())"),
                    productId = table.Column<long>(type: "bigint", nullable: false),
                    usderId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_rating", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_rating_AspNetUsers",
                        column: x => x.usderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_user_rating_product",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_userId",
                table: "order",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_orderDetail_orderId",
                table: "orderDetail",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_orderDetail_productId",
                table: "orderDetail",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_product_brandId",
                table: "product",
                column: "brandId");

            migrationBuilder.CreateIndex(
                name: "IX_product_categoryId",
                table: "product",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_product_colorId",
                table: "product",
                column: "colorId");

            migrationBuilder.CreateIndex(
                name: "IX_product_sizeId",
                table: "product",
                column: "sizeId");

            migrationBuilder.CreateIndex(
                name: "IX_product_userId",
                table: "product",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_productImg_imageId",
                table: "productImg",
                column: "imageId");

            migrationBuilder.CreateIndex(
                name: "IX_productImg_productId",
                table: "productImg",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_orderId",
                table: "transaction",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_userId",
                table: "transaction",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_user_rating_productId",
                table: "user_rating",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_user_rating_usderId",
                table: "user_rating",
                column: "usderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderDetail");

            migrationBuilder.DropTable(
                name: "productImg");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "user_rating");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "brand");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "color");

            migrationBuilder.DropTable(
                name: "size");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
