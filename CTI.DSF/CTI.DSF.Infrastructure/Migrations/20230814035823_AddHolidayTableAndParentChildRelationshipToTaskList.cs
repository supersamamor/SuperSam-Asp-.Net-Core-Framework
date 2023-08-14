using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.DSF.Infrastructure.Migrations
{
    public partial class AddHolidayTableAndParentChildRelationshipToTaskList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubTask",
                table: "TaskList",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsMilestone",
                table: "TaskList",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentTaskId",
                table: "TaskList",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Holiday",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HolidayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HolidayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_ParentTaskId",
                table: "TaskList",
                column: "ParentTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_HolidayName",
                table: "Holiday",
                column: "HolidayName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskList_TaskList_ParentTaskId",
                table: "TaskList",
                column: "ParentTaskId",
                principalTable: "TaskList",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskList_TaskList_ParentTaskId",
                table: "TaskList");

            migrationBuilder.DropTable(
                name: "Holiday");

            migrationBuilder.DropIndex(
                name: "IX_TaskList_ParentTaskId",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "IsMilestone",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "ParentTaskId",
                table: "TaskList");

            migrationBuilder.AlterColumn<string>(
                name: "SubTask",
                table: "TaskList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
