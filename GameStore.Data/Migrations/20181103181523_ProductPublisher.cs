using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class ProductPublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"1fb7dff8-1dbb-4c6e-b4e4-9f7f49813ffe", "b635297e-a0e7-4c67-b797-4cba027324c1"});

            migrationBuilder.AddColumn<string>(
                "Publisher",
                "Products",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Publisher",
                "Products");

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                    {"1fb7dff8-1dbb-4c6e-b4e4-9f7f49813ffe", "b635297e-a0e7-4c67-b797-4cba027324c1", "Admin", null});
        }
    }
}