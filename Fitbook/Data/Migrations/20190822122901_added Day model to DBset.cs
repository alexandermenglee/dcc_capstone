using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitbook.Data.Migrations
{
    public partial class addedDaymodeltoDBset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "Foods",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    DayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Meals = table.Column<int>(nullable: false),
                    Carbohydrates = table.Column<int>(nullable: false),
                    Protein = table.Column<int>(nullable: false),
                    Fat = table.Column<int>(nullable: false),
                    Calories = table.Column<int>(nullable: false),
                    FitbookUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.DayId);
                    table.ForeignKey(
                        name: "FK_Days_FitbookUsers_FitbookUserId",
                        column: x => x.FitbookUserId,
                        principalTable: "FitbookUsers",
                        principalColumn: "FitbookUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_DayId",
                table: "Foods",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Days_FitbookUserId",
                table: "Days",
                column: "FitbookUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Days_DayId",
                table: "Foods",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "DayId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Days_DayId",
                table: "Foods");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropIndex(
                name: "IX_Foods_DayId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "Foods");
        }
    }
}
