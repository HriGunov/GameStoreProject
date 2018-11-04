using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class InitialV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_AspNetUsers_ShoppingCartId",
                "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                "AccountId",
                "ShoppingCarts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_AspNetUsers_ShoppingCartId",
                "AspNetUsers",
                "ShoppingCartId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_AspNetUsers_ShoppingCartId",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "AccountId",
                "ShoppingCarts");

            migrationBuilder.CreateIndex(
                "IX_AspNetUsers_ShoppingCartId",
                "AspNetUsers",
                "ShoppingCartId");
        }
    }
}