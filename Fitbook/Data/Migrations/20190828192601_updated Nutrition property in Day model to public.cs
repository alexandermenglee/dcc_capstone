using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitbook.Data.Migrations
{
    public partial class updatedNutritionpropertyinDaymodeltopublic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NutritionMacronutrientId",
                table: "Days",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Days_NutritionMacronutrientId",
                table: "Days",
                column: "NutritionMacronutrientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_FitbookUsersMacronutrients_NutritionMacronutrientId",
                table: "Days",
                column: "NutritionMacronutrientId",
                principalTable: "FitbookUsersMacronutrients",
                principalColumn: "MacronutrientId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_FitbookUsersMacronutrients_NutritionMacronutrientId",
                table: "Days");

            migrationBuilder.DropIndex(
                name: "IX_Days_NutritionMacronutrientId",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "NutritionMacronutrientId",
                table: "Days");
        }
    }
}
