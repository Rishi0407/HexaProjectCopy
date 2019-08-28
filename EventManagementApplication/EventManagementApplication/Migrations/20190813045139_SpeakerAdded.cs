using Microsoft.EntityFrameworkCore.Migrations;

namespace EventManagementApplication.Migrations
{
    public partial class SpeakerAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Speaker",
                table: "Events",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speaker",
                table: "Events");
        }
    }
}
