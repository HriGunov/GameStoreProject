using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class ShoppingCart_Account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_ShoppingCarts_Accounts_AccountId",
                "ShoppingCarts");

            migrationBuilder.DropIndex(
                "IX_ShoppingCarts_AccountId",
                "ShoppingCarts");

            migrationBuilder.DropColumn(
                "AccountId",
                "ShoppingCarts");

            migrationBuilder.AddColumn<int>(
                "ShoppingCartId",
                "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_Accounts_ShoppingCartId",
                "Accounts",
                "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                "FK_Accounts_ShoppingCarts_ShoppingCartId",
                "Accounts",
                "ShoppingCartId",
                "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Accounts_ShoppingCarts_ShoppingCartId",
                "Accounts");

            migrationBuilder.DropIndex(
                "IX_Accounts_ShoppingCartId",
                "Accounts");

            migrationBuilder.DropColumn(
                "ShoppingCartId",
                "Accounts");

            migrationBuilder.AddColumn<int>(
                "AccountId",
                "ShoppingCarts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_ShoppingCarts_AccountId",
                "ShoppingCarts",
                "AccountId");

            migrationBuilder.AddForeignKey(
                "FK_ShoppingCarts_Accounts_AccountId",
                "ShoppingCarts",
                "AccountId",
                "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}