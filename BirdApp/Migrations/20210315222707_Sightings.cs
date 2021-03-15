using Microsoft.EntityFrameworkCore.Migrations;

namespace BirdApp.Migrations
{
    public partial class Sightings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Sightings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Sightings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
