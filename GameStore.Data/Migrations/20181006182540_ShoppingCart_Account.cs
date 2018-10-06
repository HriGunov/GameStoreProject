using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class ShoppingCart_Account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Accounts_AccountId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_AccountId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ShoppingCartId",
                table: "Accounts",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_ShoppingCarts_ShoppingCartId",
                table: "Accounts",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_ShoppingCarts_ShoppingCartId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ShoppingCartId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "ShoppingCarts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_AccountId",
                table: "ShoppingCarts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Accounts_AccountId",
                table: "ShoppingCarts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
