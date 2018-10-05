using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubSystem.Api.Migrations
{
    public partial class SimpleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name", "UniversityName" },
                values: new object[] { 1, new DateTime(2018, 10, 3, 16, 9, 13, 141, DateTimeKind.Local), new DateTime(2018, 10, 3, 16, 9, 13, 141, DateTimeKind.Local), "Science Club", "London University" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name" },
                values: new object[] { 1, new DateTime(2018, 10, 3, 16, 9, 13, 137, DateTimeKind.Local), new DateTime(2018, 10, 3, 16, 9, 13, 140, DateTimeKind.Local), "Ömrüm Baki Temiz" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
