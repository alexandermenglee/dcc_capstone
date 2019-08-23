using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitbook.Data.Migrations
{
    public partial class changedheightweightandagefromdoubletoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "FitbookUsers",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "FitbookUsers",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "FitbookUsers",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "FitbookUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Height",
                table: "FitbookUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Age",
                table: "FitbookUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
