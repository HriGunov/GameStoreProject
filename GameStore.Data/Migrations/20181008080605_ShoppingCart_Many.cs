using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class ShoppingCart_Many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Products_ShoppingCarts_ShoppingCartId",
                "Products");

            migrationBuilder.DropIndex(
                "IX_Products_ShoppingCartId",
                "Products");

            migrationBuilder.DropColumn(
                "ShoppingCartId",
                "Products");

            migrationBuilder.AddColumn<bool>(
                "IsGuest",
                "Accounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                "ShoppingCartProducts",
                table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ShoppingCartId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartProducts", x => new {x.ProductId, x.ShoppingCartId});
                    table.ForeignKey(
                        "FK_ShoppingCartProducts_Products_ProductId",
                        x => x.ProductId,
                        "Products",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ShoppingCartProducts_ShoppingCarts_ShoppingCartId",
                        x => x.ShoppingCartId,
                        "ShoppingCarts",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_ShoppingCartProducts_ShoppingCartId",
                "ShoppingCartProducts",
                "ShoppingCartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "ShoppingCartProducts");

            migrationBuilder.DropColumn(
                "IsGuest",
                "Accounts");

            migrationBuilder.AddColumn<int>(
                "ShoppingCartId",
                "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Products_ShoppingCartId",
                "Products",
                "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                "FK_Products_ShoppingCarts_ShoppingCartId",
                "Products",
                "ShoppingCartId",
                "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}