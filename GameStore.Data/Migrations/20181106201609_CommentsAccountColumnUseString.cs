using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class CommentsAccountColumnUseString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AccountId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AccountId1",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0f54d391-80ef-4acf-b986-f7a7eb6e71a9", "51886209-aef6-4d01-aa8a-65346bf2b439" });

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int));
 
            migrationBuilder.CreateIndex(
                name: "IX_Comments_AccountId",
                table: "Comments",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AccountId",
                table: "Comments",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AccountId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AccountId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1bb5b3e4-3be0-4149-8188-06cb613ce125", "325c8468-a480-40ba-8fd6-f5199aaf162c" });

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "AccountId1",
                table: "Comments",
                nullable: true);
 
            migrationBuilder.CreateIndex(
                name: "IX_Comments_AccountId1",
                table: "Comments",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AccountId1",
                table: "Comments",
                column: "AccountId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
