using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitbook.Migrations
{
    public partial class AddingCategoryIdFKToCustomRecipeModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomRecipes_Categories_CategoryId",
                table: "CustomRecipes");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CustomRecipes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CustomRecipes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CustomRecipes_Categories_CategoryId",
                table: "CustomRecipes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
