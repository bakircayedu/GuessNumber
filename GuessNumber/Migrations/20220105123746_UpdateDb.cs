using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessNumber.Migrations
{
    public partial class UpdateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchRequest_AspNetUsers_PlayerId",
                table: "MatchRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchResponse_AspNetUsers_Player1Id",
                table: "MatchResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchResponse_AspNetUsers_Player2Id",
                table: "MatchResponse");

            migrationBuilder.DropIndex(
                name: "IX_MatchResponse_Player1Id",
                table: "MatchResponse");

            migrationBuilder.DropIndex(
                name: "IX_MatchResponse_Player2Id",
                table: "MatchResponse");

            migrationBuilder.DropIndex(
                name: "IX_MatchRequest_PlayerId",
                table: "MatchRequest");

            migrationBuilder.AlterColumn<string>(
                name: "Player2Id",
                table: "MatchResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Player1Id",
                table: "MatchResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlayerId",
                table: "MatchRequest",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Player2Id",
                table: "MatchResponse",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Player1Id",
                table: "MatchResponse",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlayerId",
                table: "MatchRequest",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchResponse_Player1Id",
                table: "MatchResponse",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatchResponse_Player2Id",
                table: "MatchResponse",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRequest_PlayerId",
                table: "MatchRequest",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchRequest_AspNetUsers_PlayerId",
                table: "MatchRequest",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchResponse_AspNetUsers_Player1Id",
                table: "MatchResponse",
                column: "Player1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchResponse_AspNetUsers_Player2Id",
                table: "MatchResponse",
                column: "Player2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
