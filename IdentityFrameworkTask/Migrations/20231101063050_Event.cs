using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityFrameworkTask.Migrations
{
    public partial class Event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(nullable: true),
                    EventType = table.Column<int>(nullable: false),
                    EventStartDate = table.Column<DateTime>(nullable: false),
                    EventEndDate = table.Column<DateTime>(nullable: false),
                    RegistrationOpenDate = table.Column<DateTime>(nullable: false),
                    RegistrationCloseDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Limit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
