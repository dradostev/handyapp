using Microsoft.EntityFrameworkCore.Migrations;

namespace Handy.Infrastructure.Migrations
{
    public partial class TodoRegisterFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todo_items_todo_lists_TodoListId",
                table: "todo_items");

            migrationBuilder.RenameColumn(
                name: "TodoListId",
                table: "todo_items",
                newName: "todo_list_id");

            migrationBuilder.RenameIndex(
                name: "IX_todo_items_TodoListId",
                table: "todo_items",
                newName: "IX_todo_items_todo_list_id");

            migrationBuilder.AddForeignKey(
                name: "FK_todo_items_todo_lists_todo_list_id",
                table: "todo_items",
                column: "todo_list_id",
                principalTable: "todo_lists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todo_items_todo_lists_todo_list_id",
                table: "todo_items");

            migrationBuilder.RenameColumn(
                name: "todo_list_id",
                table: "todo_items",
                newName: "TodoListId");

            migrationBuilder.RenameIndex(
                name: "IX_todo_items_todo_list_id",
                table: "todo_items",
                newName: "IX_todo_items_TodoListId");

            migrationBuilder.AddForeignKey(
                name: "FK_todo_items_todo_lists_TodoListId",
                table: "todo_items",
                column: "TodoListId",
                principalTable: "todo_lists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
