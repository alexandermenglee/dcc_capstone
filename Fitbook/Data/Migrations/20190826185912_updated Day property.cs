using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitbook.Data.Migrations
{
    public partial class updatedDayproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "Carbohydrates",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "Fat",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "Protein",
                table: "Days");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "Days",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Carbohydrates",
                table: "Days",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fat",
                table: "Days",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Protein",
                table: "Days",
                nullable: false,
                defaultValue: 0);
        }
    }
}
