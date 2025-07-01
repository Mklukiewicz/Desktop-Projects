using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddIsFinishedToTaskItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointsHistoryDbModel_TaskItems_TaskItemId",
                table: "PointsHistoryDbModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointsHistoryDbModel",
                table: "PointsHistoryDbModel");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "PointsHistoryDbModel");

            migrationBuilder.RenameTable(
                name: "PointsHistoryDbModel",
                newName: "PointsHistory");

            migrationBuilder.RenameColumn(
                name: "When",
                table: "PointsHistory",
                newName: "Date");

            migrationBuilder.RenameIndex(
                name: "IX_PointsHistoryDbModel_TaskItemId",
                table: "PointsHistory",
                newName: "IX_PointsHistory_TaskItemId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "TaskItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointsHistory",
                table: "PointsHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PointsHistory_TaskItems_TaskItemId",
                table: "PointsHistory",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointsHistory_TaskItems_TaskItemId",
                table: "PointsHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointsHistory",
                table: "PointsHistory");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "TaskItems");

            migrationBuilder.RenameTable(
                name: "PointsHistory",
                newName: "PointsHistoryDbModel");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "PointsHistoryDbModel",
                newName: "When");

            migrationBuilder.RenameIndex(
                name: "IX_PointsHistory_TaskItemId",
                table: "PointsHistoryDbModel",
                newName: "IX_PointsHistoryDbModel_TaskItemId");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "PointsHistoryDbModel",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointsHistoryDbModel",
                table: "PointsHistoryDbModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PointsHistoryDbModel_TaskItems_TaskItemId",
                table: "PointsHistoryDbModel",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
