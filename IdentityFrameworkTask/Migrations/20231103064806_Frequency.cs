using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityFrameworkTask.Migrations
{
    public partial class Frequency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventFrequency",
                table: "Events",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventFrequency",
                table: "Events");
        }
    }
}
