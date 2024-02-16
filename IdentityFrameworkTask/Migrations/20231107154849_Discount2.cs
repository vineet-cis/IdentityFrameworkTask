using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityFrameworkTask.Migrations
{
    public partial class Discount2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Events",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Events",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
