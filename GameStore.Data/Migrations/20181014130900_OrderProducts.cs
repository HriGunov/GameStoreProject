using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class OrderProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Products_Orders_OrderId",
                "Products");

            migrationBuilder.DropIndex(
                "IX_Products_OrderId",
                "Products");

            migrationBuilder.DropColumn(
                "OrderId",
                "Products");

            migrationBuilder.CreateTable(
                "OrdersProducts",
                table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersProducts", x => new {x.OrderId, x.ProductId});
                    table.ForeignKey(
                        "FK_OrdersProducts_Orders_OrderId",
                        x => x.OrderId,
                        "Orders",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_OrdersProducts_Products_ProductId",
                        x => x.ProductId,
                        "Products",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_OrdersProducts_ProductId",
                "OrdersProducts",
                "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "OrdersProducts");

            migrationBuilder.AddColumn<int>(
                "OrderId",
                "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Products_OrderId",
                "Products",
                "OrderId");

            migrationBuilder.AddForeignKey(
                "FK_Products_Orders_OrderId",
                "Products",
                "OrderId",
                "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}