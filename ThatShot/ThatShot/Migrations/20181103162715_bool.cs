using Microsoft.EntityFrameworkCore.Migrations;

namespace ThatShot.Migrations
{
    public partial class @bool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "show",
                table: "Pictures",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "show",
                table: "Pictures",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
