using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitbook.Migrations
{
    public partial class AddingCategoryIdFKToCustomRecipeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "CustomRecipes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomRecipes_CategoryId",
                table: "CustomRecipes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomRecipes_Categories_CategoryId",
                table: "CustomRecipes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomRecipes_Categories_CategoryId",
                table: "CustomRecipes");

            migrationBuilder.DropIndex(
                name: "IX_CustomRecipes_CategoryId",
                table: "CustomRecipes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "CustomRecipes");
        }
    }
}
