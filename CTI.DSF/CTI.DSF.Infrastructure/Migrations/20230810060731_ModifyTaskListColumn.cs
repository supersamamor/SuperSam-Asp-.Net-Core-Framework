using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.DSF.Infrastructure.Migrations
{
    public partial class ModifyTaskListColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskType",
                table: "TaskList",
                newName: "TaskClassification");

            migrationBuilder.AddColumn<string>(
                name: "SubTask",
                table: "TaskList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubTask",
                table: "TaskList");

            migrationBuilder.RenameColumn(
                name: "TaskClassification",
                table: "TaskList",
                newName: "TaskType");
        }
    }
}
