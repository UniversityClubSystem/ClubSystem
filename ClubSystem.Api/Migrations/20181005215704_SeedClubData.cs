using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubSystem.Api.Migrations
{
    public partial class SeedClubData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name", "UniversityName" },
                values: new object[] { 1, new DateTime(2018, 10, 6, 0, 57, 3, 370, DateTimeKind.Local), new DateTime(2018, 10, 6, 0, 57, 3, 370, DateTimeKind.Local), "Space Club", "London University" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2018, 10, 6, 0, 57, 3, 367, DateTimeKind.Local), new DateTime(2018, 10, 6, 0, 57, 3, 369, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate", "Name" },
                values: new object[] { new DateTime(2018, 10, 6, 0, 57, 3, 370, DateTimeKind.Local), new DateTime(2018, 10, 6, 0, 57, 3, 370, DateTimeKind.Local), "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2018, 10, 6, 0, 48, 42, 22, DateTimeKind.Local), new DateTime(2018, 10, 6, 0, 48, 42, 24, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate", "Name" },
                values: new object[] { new DateTime(2018, 10, 6, 0, 48, 42, 26, DateTimeKind.Local), new DateTime(2018, 10, 6, 0, 48, 42, 26, DateTimeKind.Local), "Ömrüm Baki Temiz" });
        }
    }
}
