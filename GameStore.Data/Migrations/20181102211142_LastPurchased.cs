using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class LastPurchased : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"752e3287-1772-4142-8a87-5f01c8e8a36e", "98e9f45a-277c-4596-baa8-ef10a9acc31d"});

            migrationBuilder.DropColumn(
                "IsAdmin",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "IsGuest",
                "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                "Name",
                "Products",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

            migrationBuilder.AddColumn<DateTime>(
                "LastPurchased",
                "Products",
                nullable: true);

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                    {"1fb7dff8-1dbb-4c6e-b4e4-9f7f49813ffe", "b635297e-a0e7-4c67-b797-4cba027324c1", "Admin", null});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"1fb7dff8-1dbb-4c6e-b4e4-9f7f49813ffe", "b635297e-a0e7-4c67-b797-4cba027324c1"});

            migrationBuilder.DropColumn(
                "LastPurchased",
                "Products");

            migrationBuilder.AlterColumn<string>(
                "Name",
                "Products",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<bool>(
                "IsAdmin",
                "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                "IsGuest",
                "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                    {"752e3287-1772-4142-8a87-5f01c8e8a36e", "98e9f45a-277c-4596-baa8-ef10a9acc31d", "Admin", null});
        }
    }
}