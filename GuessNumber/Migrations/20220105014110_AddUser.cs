using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessNumber.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayMove_MatchResponse_MatchResponseId",
                table: "GamePlayMove");

            migrationBuilder.DropForeignKey(
                name: "FK_GameResult_MatchResponse_MatchResponseId",
                table: "GameResult");

            migrationBuilder.DropColumn(
                name: "Player1",
                table: "MatchResponse");

            migrationBuilder.DropColumn(
                name: "Player2",
                table: "MatchResponse");

            migrationBuilder.DropColumn(
                name: "MathcResponseId",
                table: "GameResult");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "GamePlayMove");

            migrationBuilder.AddColumn<string>(
                name: "Player1Id",
                table: "MatchResponse",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Player2Id",
                table: "MatchResponse",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlayerId",
                table: "MatchRequest",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MatchResponseId",
                table: "GameResult",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlayerId",
                table: "GamePlayMove",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "MatchResponseId",
                table: "GamePlayMove",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
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

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayMove_PlayerId",
                table: "GamePlayMove",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayMove_AspNetUsers_PlayerId",
                table: "GamePlayMove",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayMove_MatchResponse_MatchResponseId",
                table: "GamePlayMove",
                column: "MatchResponseId",
                principalTable: "MatchResponse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameResult_MatchResponse_MatchResponseId",
                table: "GameResult",
                column: "MatchResponseId",
                principalTable: "MatchResponse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayMove_AspNetUsers_PlayerId",
                table: "GamePlayMove");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayMove_MatchResponse_MatchResponseId",
                table: "GamePlayMove");

            migrationBuilder.DropForeignKey(
                name: "FK_GameResult_MatchResponse_MatchResponseId",
                table: "GameResult");

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

            migrationBuilder.DropIndex(
                name: "IX_GamePlayMove_PlayerId",
                table: "GamePlayMove");

            migrationBuilder.DropColumn(
                name: "Player1Id",
                table: "MatchResponse");

            migrationBuilder.DropColumn(
                name: "Player2Id",
                table: "MatchResponse");

            migrationBuilder.AddColumn<string>(
                name: "Player1",
                table: "MatchResponse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Player2",
                table: "MatchResponse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlayerId",
                table: "MatchRequest",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MatchResponseId",
                table: "GameResult",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MathcResponseId",
                table: "GameResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PlayerId",
                table: "GamePlayMove",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "MatchResponseId",
                table: "GamePlayMove",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "GamePlayMove",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayMove_MatchResponse_MatchResponseId",
                table: "GamePlayMove",
                column: "MatchResponseId",
                principalTable: "MatchResponse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameResult_MatchResponse_MatchResponseId",
                table: "GameResult",
                column: "MatchResponseId",
                principalTable: "MatchResponse",
                principalColumn: "Id");
        }
    }
}
