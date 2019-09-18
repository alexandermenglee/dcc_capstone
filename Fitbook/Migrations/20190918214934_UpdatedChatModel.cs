using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitbook.Migrations
{
    public partial class UpdatedChatModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FitbookUsers_Chats_ChatId",
                table: "FitbookUsers");

            migrationBuilder.DropIndex(
                name: "IX_FitbookUsers_ChatId",
                table: "FitbookUsers");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "FitbookUsers");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "FitbookUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FitbookUsers_ChatId",
                table: "FitbookUsers",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_FitbookUsers_Chats_ChatId",
                table: "FitbookUsers",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
