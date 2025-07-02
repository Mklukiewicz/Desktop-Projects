using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddRemainingTimeToTaskItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "TaskItem",
                newName: "IsMarked");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "RemainingTime",
                table: "TaskItems",
                type: "interval",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingTime",
                table: "TaskItems");

            migrationBuilder.RenameColumn(
                name: "IsMarked",
                table: "TaskItem",
                newName: "IsCompleted");
        }
    }
}
