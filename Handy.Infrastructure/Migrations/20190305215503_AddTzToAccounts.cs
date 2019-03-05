using Microsoft.EntityFrameworkCore.Migrations;

namespace Handy.Infrastructure.Migrations
{
    public partial class AddTzToAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "tz",
                table: "accounts",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tz",
                table: "accounts");
        }
    }
}
