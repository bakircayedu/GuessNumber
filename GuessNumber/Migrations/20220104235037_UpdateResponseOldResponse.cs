using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessNumber.Migrations
{
    public partial class UpdateResponseOldResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsOldResponse",
                table: "MatchResponse",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOldResponse",
                table: "MatchResponse");
        }
    }
}
