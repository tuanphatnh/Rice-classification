using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileShopAPI.Migrations
{
    public partial class mig11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderDetail");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "avatar_url",
                table: "AspNetUsers",
                newName: "AvatarUrl");

            migrationBuilder.AddColumn<string>(
                name: "packageId",
                table: "transaction",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "priorities",
                table: "product",
                type: "int",
                nullable: true,
                defaultValueSql: "((0))");

            migrationBuilder.AddColumn<DateTime>(
                name: "updateDate",
                table: "product",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "order",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "order",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "order",
                type: "int",
                nullable: true,
                comment: "1 = trả hết, 2 = đặt cọc");

            migrationBuilder.AddColumn<string>(
                name: "userFullName",
                table: "order",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "isCover",
                table: "image",
                type: "bit",
                nullable: false,
                comment: "hình ảnh là ảnh bìa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((0))",
                oldComment: "hình ảnh là ảnh bìa");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "image",
                type: "bigint",
                nullable: false,
                defaultValueSql: "(CONVERT([bigint],(0)))",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "HexValue",
                table: "color",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "(N'')",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "UserBalance",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "coin_action",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    action_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ca_coin_amount = table.Column<int>(type: "int", nullable: true),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())"),
                    updatedDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coin_action", x => x.id);
                },
                comment: "Actions on website using coin");

            migrationBuilder.CreateTable(
                name: "coin_package",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    package_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    package_value = table.Column<long>(type: "bigint", nullable: false),
                    value_unit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "('VND')", comment: "vnđ,...v.v"),
                    coin_amount = table.Column<long>(type: "bigint", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())"),
                    updatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coin_package", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_order",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<long>(type: "bigint", nullable: false),
                    orderId = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_order", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_order_order",
                        column: x => x.orderId,
                        principalTable: "order",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_product_order_product",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "report_category",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())"),
                    updatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shipping_address",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    address_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    isDefault = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    wardId = table.Column<int>(type: "int", nullable: false),
                    address_detail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "house number, district name"),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())"),
                    updatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping_address", x => x.id);
                    table.ForeignKey(
                        name: "fk_shipping_address_AspNetUsers",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "subscription_package",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postAmout = table.Column<int>(type: "int", nullable: false, comment: "Số lượng được tin đăng khi mua gói"),
                    expiredIn = table.Column<int>(type: "int", nullable: false, comment: "Số ngày sử dụng"),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())"),
                    updatedDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscription_package", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "report",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subject = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reportedUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    reportedProductId = table.Column<long>(type: "bigint", nullable: true),
                    userId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false, comment: "user id of user who sent the report"),
                    reportCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Report_AspNetUsers",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_Report_report_category",
                        column: x => x.reportCategoryId,
                        principalTable: "report_category",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "active_subscription",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usedPost = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))", comment: "Số lượng bài đăng đã sử dụng"),
                    expiredDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    activatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())"),
                    userId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    sp_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_active_subscription", x => x.id);
                    table.ForeignKey(
                        name: "fk_active_subcription_AspNetUsers",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_active_subcription_subscription_package",
                        column: x => x.sp_id,
                        principalTable: "subscription_package",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "internal_transaction",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    coinActionId = table.Column<long>(type: "bigint", nullable: true),
                    spId = table.Column<long>(type: "bigint", nullable: true),
                    it_amount = table.Column<int>(type: "int", nullable: true),
                    it_info = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    it_secureHash = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_internal_transaction", x => x.id);
                    table.ForeignKey(
                        name: "fk_internal_transaction_AspNetUsers",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_internal_transaction_coin_action",
                        column: x => x.coinActionId,
                        principalTable: "coin_action",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_internal_transaction_subscription_package",
                        column: x => x.spId,
                        principalTable: "subscription_package",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "evidence",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    image_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reportId = table.Column<long>(type: "bigint", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evidence", x => x.id);
                    table.ForeignKey(
                        name: "fk_evidence_Report",
                        column: x => x.reportId,
                        principalTable: "report",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_transaction_packageId",
                table: "transaction",
                column: "packageId");

            migrationBuilder.CreateIndex(
                name: "IX_active_subscription_sp_id",
                table: "active_subscription",
                column: "sp_id");

            migrationBuilder.CreateIndex(
                name: "IX_active_subscription_userId",
                table: "active_subscription",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_evidence_reportId",
                table: "evidence",
                column: "reportId");

            migrationBuilder.CreateIndex(
                name: "IX_internal_transaction_coinActionId",
                table: "internal_transaction",
                column: "coinActionId");

            migrationBuilder.CreateIndex(
                name: "IX_internal_transaction_spId",
                table: "internal_transaction",
                column: "spId");

            migrationBuilder.CreateIndex(
                name: "IX_internal_transaction_userId",
                table: "internal_transaction",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_product_order_orderId",
                table: "product_order",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_product_order_productId",
                table: "product_order",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_report_reportCategoryId",
                table: "report",
                column: "reportCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_report_userId",
                table: "report",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_address_userId",
                table: "shipping_address",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "fk_vnp_transaction_coin_package",
                table: "transaction",
                column: "packageId",
                principalTable: "coin_package",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_vnp_transaction_coin_package",
                table: "transaction");

            migrationBuilder.DropTable(
                name: "active_subscription");

            migrationBuilder.DropTable(
                name: "coin_package");

            migrationBuilder.DropTable(
                name: "evidence");

            migrationBuilder.DropTable(
                name: "internal_transaction");

            migrationBuilder.DropTable(
                name: "product_order");

            migrationBuilder.DropTable(
                name: "shipping_address");

            migrationBuilder.DropTable(
                name: "report");

            migrationBuilder.DropTable(
                name: "coin_action");

            migrationBuilder.DropTable(
                name: "subscription_package");

            migrationBuilder.DropTable(
                name: "report_category");

            migrationBuilder.DropIndex(
                name: "IX_transaction_packageId",
                table: "transaction");

            migrationBuilder.DropColumn(
                name: "packageId",
                table: "transaction");

            migrationBuilder.DropColumn(
                name: "priorities",
                table: "product");

            migrationBuilder.DropColumn(
                name: "updateDate",
                table: "product");

            migrationBuilder.DropColumn(
                name: "address",
                table: "order");

            migrationBuilder.DropColumn(
                name: "email",
                table: "order");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "order");

            migrationBuilder.DropColumn(
                name: "type",
                table: "order");

            migrationBuilder.DropColumn(
                name: "userFullName",
                table: "order");

            migrationBuilder.DropColumn(
                name: "UserBalance",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AvatarUrl",
                table: "AspNetUsers",
                newName: "avatar_url");

            migrationBuilder.AlterColumn<bool>(
                name: "isCover",
                table: "image",
                type: "bit",
                nullable: false,
                defaultValueSql: "((0))",
                comment: "hình ảnh là ảnh bìa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "hình ảnh là ảnh bìa");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "image",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValueSql: "(CONVERT([bigint],(0)))");

            migrationBuilder.AlterColumn<string>(
                name: "HexValue",
                table: "color",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "(N'')");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "orderDetail",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderId = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    productId = table.Column<long>(type: "bigint", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_orderDetail_orderId",
                table: "orderDetail",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_orderDetail_productId",
                table: "orderDetail",
                column: "productId");
        }
    }
}
