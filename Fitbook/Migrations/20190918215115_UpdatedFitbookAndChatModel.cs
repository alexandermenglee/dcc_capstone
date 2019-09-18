using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitbook.Migrations
{
    public partial class UpdatedFitbookAndChatModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_FitbookUsers_FitbookUserId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_FitbookUserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "FitbookUserId",
                table: "Chats");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FitbookUserId",
                table: "Chats",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_FitbookUserId",
                table: "Chats",
                column: "FitbookUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_FitbookUsers_FitbookUserId",
                table: "Chats",
                column: "FitbookUserId",
                principalTable: "FitbookUsers",
                principalColumn: "FitbookUserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
