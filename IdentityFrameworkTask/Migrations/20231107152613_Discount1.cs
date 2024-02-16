using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityFrameworkTask.Migrations
{
    public partial class Discount1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Events",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Events",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
