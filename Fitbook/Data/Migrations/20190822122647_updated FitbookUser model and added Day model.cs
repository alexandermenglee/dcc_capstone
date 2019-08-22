using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitbook.Data.Migrations
{
    public partial class updatedFitbookUsermodelandaddedDaymodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Meals",
                table: "FitbookUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Meals",
                table: "FitbookUsers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
