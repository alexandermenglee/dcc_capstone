using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitbook.Migrations
{
    public partial class MealTableSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "MealId", "DayId" },
                values: new object[] { -1, null });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "MealId", "DayId" },
                values: new object[] { -2, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "MealId",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "MealId",
                keyValue: -1);
        }
    }
}
