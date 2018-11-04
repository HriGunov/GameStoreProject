using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class OrdersToUseSringBasedAccountID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Orders_AspNetUsers_AccountId1",
                "Orders");

            migrationBuilder.DropIndex(
                "IX_Orders_AccountId1",
                "Orders");

            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"752e3287-1772-4142-8a87-5f01c8e8a36e", "98e9f45a-277c-4596-baa8-ef10a9acc31d"});

            migrationBuilder.DropColumn(
                "AccountId1",
                "Orders");

            migrationBuilder.AlterColumn<string>(
                "AccountId",
                "Orders",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                    {"0f54d391-80ef-4acf-b986-f7a7eb6e71a9", "51886209-aef6-4d01-aa8a-65346bf2b439", "Admin", null});

            migrationBuilder.CreateIndex(
                "IX_Orders_AccountId",
                "Orders",
                "AccountId");

            migrationBuilder.AddForeignKey(
                "FK_Orders_AspNetUsers_AccountId",
                "Orders",
                "AccountId",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Orders_AspNetUsers_AccountId",
                "Orders");

            migrationBuilder.DropIndex(
                "IX_Orders_AccountId",
                "Orders");

            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"0f54d391-80ef-4acf-b986-f7a7eb6e71a9", "51886209-aef6-4d01-aa8a-65346bf2b439"});

            migrationBuilder.AlterColumn<int>(
                "AccountId",
                "Orders",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                "AccountId1",
                "Orders",
                nullable: true);

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                    {"752e3287-1772-4142-8a87-5f01c8e8a36e", "98e9f45a-277c-4596-baa8-ef10a9acc31d", "Admin", null});

            migrationBuilder.CreateIndex(
                "IX_Orders_AccountId1",
                "Orders",
                "AccountId1");

            migrationBuilder.AddForeignKey(
                "FK_Orders_AspNetUsers_AccountId1",
                "Orders",
                "AccountId1",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}