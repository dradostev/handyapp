using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Handy.Infrastructure.Migrations
{
    public partial class NewDbInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    login = table.Column<string>(nullable: true),
                    password_hash = table.Column<string>(nullable: true),
                    screen_name = table.Column<string>(nullable: true),
                    telegram_chat_id = table.Column<long>(nullable: false),
                    registered = table.Column<DateTime>(nullable: false),
                    modified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notes",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    account_id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    content = table.Column<string>(nullable: true),
                    telegram_message_id = table.Column<int>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    modified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => x.id);
                    table.ForeignKey(
                        name: "FK_notes_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reminders",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    account_id = table.Column<Guid>(nullable: false),
                    content = table.Column<string>(nullable: true),
                    fire_on = table.Column<DateTime>(nullable: false),
                    enabled = table.Column<bool>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    modified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reminders", x => x.id);
                    table.ForeignKey(
                        name: "FK_reminders_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_login",
                table: "accounts",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notes_account_id",
                table: "notes",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_reminders_account_id",
                table: "reminders",
                column: "account_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.DropTable(
                name: "reminders");

            migrationBuilder.DropTable(
                name: "accounts");
        }
    }
}
