using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubSystem.Api.Migrations
{
    public partial class UpdateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clubs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clubs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name", "UniversityName" },
                values: new object[] { 1, new DateTime(2018, 10, 6, 0, 57, 3, 370, DateTimeKind.Local), new DateTime(2018, 10, 6, 0, 57, 3, 370, DateTimeKind.Local), "Space Club", "London University" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name" },
                values: new object[] { 1, new DateTime(2018, 10, 6, 0, 57, 3, 367, DateTimeKind.Local), new DateTime(2018, 10, 6, 0, 57, 3, 369, DateTimeKind.Local), "Ömrüm Baki Temiz" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Name" },
                values: new object[] { 2, new DateTime(2018, 10, 6, 0, 57, 3, 370, DateTimeKind.Local), new DateTime(2018, 10, 6, 0, 57, 3, 370, DateTimeKind.Local), "admin" });
        }
    }
}
