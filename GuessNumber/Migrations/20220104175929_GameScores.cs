using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessNumber.Migrations
{
    public partial class GameScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuessNumberUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MathcResponseId = table.Column<int>(type: "int", nullable: false),
                    GamePoint = table.Column<int>(type: "int", nullable: false),
                    MatchResponseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameResult_AspNetUsers_GuessNumberUserId",
                        column: x => x.GuessNumberUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameResult_MatchResponse_MatchResponseId",
                        column: x => x.MatchResponseId,
                        principalTable: "MatchResponse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameResult_GuessNumberUserId",
                table: "GameResult",
                column: "GuessNumberUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameResult_MatchResponseId",
                table: "GameResult",
                column: "MatchResponseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameResult");
        }
    }
}
