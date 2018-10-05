using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubSystem.Api.Migrations
{
    public partial class SeedUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name" },
                values: new object[] { 1, new DateTime(2018, 10, 6, 0, 48, 42, 22, DateTimeKind.Local), new DateTime(2018, 10, 6, 0, 48, 42, 24, DateTimeKind.Local), "Ömrüm Baki Temiz" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name" },
                values: new object[] { 2, new DateTime(2018, 10, 6, 0, 48, 42, 26, DateTimeKind.Local), new DateTime(2018, 10, 6, 0, 48, 42, 26, DateTimeKind.Local), "Ömrüm Baki Temiz" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
