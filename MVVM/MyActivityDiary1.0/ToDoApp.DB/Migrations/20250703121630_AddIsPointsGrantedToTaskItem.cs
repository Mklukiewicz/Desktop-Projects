using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPointsGrantedToTaskItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPointsGranted",
                table: "TaskItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPointsGranted",
                table: "TaskItem",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPointsGranted",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "IsPointsGranted",
                table: "TaskItem");
        }
    }
}
