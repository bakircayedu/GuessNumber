using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessNumber.Migrations
{
    public partial class DeleteResponseAtt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOldResponse",
                table: "MatchResponse");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsOldResponse",
                table: "MatchResponse",
                type: "int",
                nullable: true);
        }
    }
}
