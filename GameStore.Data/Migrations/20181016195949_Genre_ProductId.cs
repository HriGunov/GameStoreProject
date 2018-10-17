using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class Genre_ProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Genres_Products_ProductId",
                "Genres");

            migrationBuilder.AlterColumn<int>(
                "ProductId",
                "Genres",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                "FK_Genres_Products_ProductId",
                "Genres",
                "ProductId",
                "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Genres_Products_ProductId",
                "Genres");

            migrationBuilder.AlterColumn<int>(
                "ProductId",
                "Genres",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                "FK_Genres_Products_ProductId",
                "Genres",
                "ProductId",
                "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}