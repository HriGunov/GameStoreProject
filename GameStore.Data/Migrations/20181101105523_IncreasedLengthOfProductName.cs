using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class IncreasedLengthOfProductName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Name",
                "Products",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Name",
                "Products",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);
        }
    }
}