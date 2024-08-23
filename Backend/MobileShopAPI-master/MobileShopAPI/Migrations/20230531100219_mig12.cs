using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileShopAPI.Migrations
{
    public partial class mig12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.CreateTable(
                name: "vnp_transaction",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    vnp_SecureHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    packageId = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vnp_transaction", x => x.id);
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
                    table.ForeignKey(
                        name: "fk_vnp_transaction_coin_package",
                        column: x => x.packageId,
                        principalTable: "coin_package",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_transaction_orderId",
                table: "vnp_transaction",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_userId",
                table: "vnp_transaction",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_vnp_transaction_packageId",
                table: "vnp_transaction",
                column: "packageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vnp_transaction");

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    orderId = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    packageId = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    vnp_Amount = table.Column<long>(type: "bigint", nullable: true),
                    vnp_BankCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    vnp_Command = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    vnp_CreateDate = table.Column<long>(type: "bigint", nullable: true),
                    vnp_CurrCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    vnp_IpAddr = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    vnp_Locale = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    vnp_OrderInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vnp_OrderType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    vnp_SecureHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vnp_TmnCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    vnp_TxnRef = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    vnp_Version = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
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
                    table.ForeignKey(
                        name: "fk_vnp_transaction_coin_package",
                        column: x => x.packageId,
                        principalTable: "coin_package",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_transaction_orderId",
                table: "transaction",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_packageId",
                table: "transaction",
                column: "packageId");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_userId",
                table: "transaction",
                column: "userId");
        }
    }
}
