using Microsoft.EntityFrameworkCore.Migrations;

namespace BirdApp.Migrations
{
    public partial class Seeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sightings_Species_NameBirdID",
                table: "Sightings");

            migrationBuilder.DropForeignKey(
                name: "FK_Species_AspNetUsers_BirdWatcherId",
                table: "Species");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Species",
                table: "Species");

            migrationBuilder.RenameTable(
                name: "Species",
                newName: "Birds");

            migrationBuilder.RenameIndex(
                name: "IX_Species_BirdWatcherId",
                table: "Birds",
                newName: "IX_Birds_BirdWatcherId");

            migrationBuilder.AddColumn<string>(
                name: "BirdPicture",
                table: "Birds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Birds",
                table: "Birds",
                column: "BirdID");

            migrationBuilder.AddForeignKey(
                name: "FK_Birds_AspNetUsers_BirdWatcherId",
                table: "Birds",
                column: "BirdWatcherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sightings_Birds_NameBirdID",
                table: "Sightings",
                column: "NameBirdID",
                principalTable: "Birds",
                principalColumn: "BirdID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birds_AspNetUsers_BirdWatcherId",
                table: "Birds");

            migrationBuilder.DropForeignKey(
                name: "FK_Sightings_Birds_NameBirdID",
                table: "Sightings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Birds",
                table: "Birds");

            migrationBuilder.DropColumn(
                name: "BirdPicture",
                table: "Birds");

            migrationBuilder.RenameTable(
                name: "Birds",
                newName: "Species");

            migrationBuilder.RenameIndex(
                name: "IX_Birds_BirdWatcherId",
                table: "Species",
                newName: "IX_Species_BirdWatcherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Species",
                table: "Species",
                column: "BirdID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sightings_Species_NameBirdID",
                table: "Sightings",
                column: "NameBirdID",
                principalTable: "Species",
                principalColumn: "BirdID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Species_AspNetUsers_BirdWatcherId",
                table: "Species",
                column: "BirdWatcherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
