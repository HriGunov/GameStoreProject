using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class CreatedOn_Account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Accounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
