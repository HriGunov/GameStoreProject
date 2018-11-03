using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class OrdersToUseSringBasedAccountID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_AccountId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AccountId1",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "752e3287-1772-4142-8a87-5f01c8e8a36e", "98e9f45a-277c-4596-baa8-ef10a9acc31d" });

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0f54d391-80ef-4acf-b986-f7a7eb6e71a9", "51886209-aef6-4d01-aa8a-65346bf2b439", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountId",
                table: "Orders",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_AccountId",
                table: "Orders",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_AccountId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AccountId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0f54d391-80ef-4acf-b986-f7a7eb6e71a9", "51886209-aef6-4d01-aa8a-65346bf2b439" });

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "AccountId1",
                table: "Orders",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "752e3287-1772-4142-8a87-5f01c8e8a36e", "98e9f45a-277c-4596-baa8-ef10a9acc31d", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountId1",
                table: "Orders",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_AccountId1",
                table: "Orders",
                column: "AccountId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
