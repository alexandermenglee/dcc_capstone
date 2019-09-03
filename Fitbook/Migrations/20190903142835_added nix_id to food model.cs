using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitbook.Migrations
{
    public partial class addednix_idtofoodmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NIX_ID",
                table: "Foods",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NIX_ID",
                table: "Foods");
        }
    }
}
