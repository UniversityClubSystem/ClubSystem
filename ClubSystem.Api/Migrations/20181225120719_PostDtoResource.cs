using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubSystem.Api.Migrations
{
    public partial class PostDtoResource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubPosts");

            migrationBuilder.AlterColumn<string>(
                name: "MediaId",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "ClubId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClubId",
                table: "Clubs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ClubId",
                table: "Posts",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_ClubId",
                table: "Clubs",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Clubs_ClubId",
                table: "Clubs",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Clubs_ClubId",
                table: "Posts",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Clubs_ClubId",
                table: "Clubs");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Clubs_ClubId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ClubId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_ClubId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Clubs");

            migrationBuilder.AlterColumn<int>(
                name: "MediaId",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ClubPosts",
                columns: table => new
                {
                    ClubId = table.Column<string>(nullable: false),
                    PostId = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubPosts", x => new { x.ClubId, x.PostId });
                    table.ForeignKey(
                        name: "FK_ClubPosts_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubPosts_PostId",
                table: "ClubPosts",
                column: "PostId");
        }
    }
}
