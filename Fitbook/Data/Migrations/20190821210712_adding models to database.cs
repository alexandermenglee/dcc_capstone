using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitbook.Data.Migrations
{
    public partial class addingmodelstodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FitbookUsers",
                columns: table => new
                {
                    FitbookUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Age = table.Column<double>(nullable: true),
                    Height = table.Column<double>(nullable: true),
                    Weight = table.Column<double>(nullable: true),
                    Meals = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitbookUsers", x => x.FitbookUserId);
                    table.ForeignKey(
                        name: "FK_FitbookUsers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    FoodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FoodName = table.Column<string>(nullable: true),
                    Carbohydrates = table.Column<int>(nullable: false),
                    Protein = table.Column<int>(nullable: false),
                    Fat = table.Column<int>(nullable: false),
                    Calories = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.FoodId);
                });

            migrationBuilder.CreateTable(
                name: "ReccommendedRecipes",
                columns: table => new
                {
                    RecommendedRecipeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReccommendedRecipes", x => x.RecommendedRecipeId);
                });

            migrationBuilder.CreateTable(
                name: "CustomRecipes",
                columns: table => new
                {
                    CustomRecipeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomRecipeName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "varchar(500)", nullable: true),
                    Carbohydrates = table.Column<int>(nullable: false),
                    Protein = table.Column<int>(nullable: false),
                    Fat = table.Column<int>(nullable: false),
                    Calories = table.Column<int>(nullable: false),
                    Servings = table.Column<double>(nullable: true),
                    CaloriesPerServing = table.Column<int>(nullable: false),
                    FitbookUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomRecipes", x => x.CustomRecipeId);
                    table.ForeignKey(
                        name: "FK_CustomRecipes_FitbookUsers_FitbookUserId",
                        column: x => x.FitbookUserId,
                        principalTable: "FitbookUsers",
                        principalColumn: "FitbookUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FitbookUsersMacronutrients",
                columns: table => new
                {
                    MacronutrientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Carbohydrates = table.Column<int>(nullable: false),
                    Protein = table.Column<int>(nullable: false),
                    Fat = table.Column<int>(nullable: false),
                    DailyCalories = table.Column<int>(nullable: false),
                    FitbookUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitbookUsersMacronutrients", x => x.MacronutrientId);
                    table.ForeignKey(
                        name: "FK_FitbookUsersMacronutrients_FitbookUsers_FitbookUserId",
                        column: x => x.FitbookUserId,
                        principalTable: "FitbookUsers",
                        principalColumn: "FitbookUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomRecipes_FitbookUserId",
                table: "CustomRecipes",
                column: "FitbookUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FitbookUsers_ApplicationUserId",
                table: "FitbookUsers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FitbookUsersMacronutrients_FitbookUserId",
                table: "FitbookUsersMacronutrients",
                column: "FitbookUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomRecipes");

            migrationBuilder.DropTable(
                name: "FitbookUsersMacronutrients");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "ReccommendedRecipes");

            migrationBuilder.DropTable(
                name: "FitbookUsers");
        }
    }
}
