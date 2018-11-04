using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class ProductPublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1fb7dff8-1dbb-4c6e-b4e4-9f7f49813ffe", "b635297e-a0e7-4c67-b797-4cba027324c1" });

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Products",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1fb7dff8-1dbb-4c6e-b4e4-9f7f49813ffe", "b635297e-a0e7-4c67-b797-4cba027324c1", "Admin", null });
        }
    }
}
