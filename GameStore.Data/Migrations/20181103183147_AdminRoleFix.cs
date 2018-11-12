using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class AdminRoleFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                    {"9fa7402b-4770-4029-a851-631946f4e4b7", "e00e688e-0118-403c-aa69-2330c0cf7037", "Admin", "ADMIN"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"9fa7402b-4770-4029-a851-631946f4e4b7", "e00e688e-0118-403c-aa69-2330c0cf7037"});
        }
    }
}