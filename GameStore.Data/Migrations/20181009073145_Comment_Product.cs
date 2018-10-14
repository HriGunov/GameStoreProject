using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class Comment_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Comments_Products_ProductId",
                "Comments");

            migrationBuilder.AlterColumn<int>(
                "ProductId",
                "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                "FK_Comments_Products_ProductId",
                "Comments",
                "ProductId",
                "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Comments_Products_ProductId",
                "Comments");

            migrationBuilder.AlterColumn<int>(
                "ProductId",
                "Comments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                "FK_Comments_Products_ProductId",
                "Comments",
                "ProductId",
                "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}