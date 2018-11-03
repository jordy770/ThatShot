using Microsoft.EntityFrameworkCore.Migrations;

namespace ThatShot.Migrations
{
    public partial class show : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "show",
                table: "Pictures",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "show",
                table: "Pictures");
        }
    }
}
