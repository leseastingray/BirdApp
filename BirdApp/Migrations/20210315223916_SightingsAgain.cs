using Microsoft.EntityFrameworkCore.Migrations;

namespace BirdApp.Migrations
{
    public partial class SightingsAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BirdName",
                table: "Sightings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirdName",
                table: "Sightings");
        }
    }
}
