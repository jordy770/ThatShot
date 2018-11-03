using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThatShot.Migrations
{
    public partial class DelVirtual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_AspNetUsers_TSUserId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_TSUserId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "TSUserId",
                table: "Pictures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TSUserId",
                table: "Pictures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_TSUserId",
                table: "Pictures",
                column: "TSUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_AspNetUsers_TSUserId",
                table: "Pictures",
                column: "TSUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
