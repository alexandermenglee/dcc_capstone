using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitbook.Migrations
{
    public partial class FoodTableSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "FoodId", "Amount", "Calories", "Carbohydrates", "Fat", "FoodName", "Protein" },
                values: new object[] { -1, 4, 0, 0, 1, "chicken breast", 25 });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "FoodId", "Amount", "Calories", "Carbohydrates", "Fat", "FoodName", "Protein" },
                values: new object[] { -2, 1, 0, 37, 0, "brown rice", 7 });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "FoodId", "Amount", "Calories", "Carbohydrates", "Fat", "FoodName", "Protein" },
                values: new object[] { -3, 2, 0, 0, 16, "olive oil", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "FoodId",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "FoodId",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "FoodId",
                keyValue: -1);
        }
    }
}
