using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessNumber.Migrations
{
    public partial class GamePlayMove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamePlayMove",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerMove = table.Column<int>(type: "int", nullable: false),
                    TurnCount = table.Column<int>(type: "int", nullable: false),
                    MoveTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MatchResponseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayMove", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamePlayMove_MatchResponse_MatchResponseId",
                        column: x => x.MatchResponseId,
                        principalTable: "MatchResponse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayMove_MatchResponseId",
                table: "GamePlayMove",
                column: "MatchResponseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlayMove");
        }
    }
}
