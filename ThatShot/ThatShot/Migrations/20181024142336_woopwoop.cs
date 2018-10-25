using Microsoft.EntityFrameworkCore.Migrations;

namespace ThatShot.Migrations
{
    public partial class woopwoop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_AspNetUsers_UserId",
                table: "Pictures");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Pictures",
                newName: "TSUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Pictures_UserId",
                table: "Pictures",
                newName: "IX_Pictures_TSUserId");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Pictures",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_AspNetUsers_TSUserId",
                table: "Pictures",
                column: "TSUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_AspNetUsers_TSUserId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Pictures");

            migrationBuilder.RenameColumn(
                name: "TSUserId",
                table: "Pictures",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Pictures_TSUserId",
                table: "Pictures",
                newName: "IX_Pictures_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_AspNetUsers_UserId",
                table: "Pictures",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
