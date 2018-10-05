using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class DeletedBy_Account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Accounts");
        }
    }
}
