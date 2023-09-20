using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Assignment_AssignmentCode",
                table: "Delivery");

            migrationBuilder.DropIndex(
                name: "IX_Delivery_AssignmentCode",
                table: "Delivery");

            migrationBuilder.AlterColumn<string>(
                name: "AssignmentCode",
                table: "Delivery",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)");

            migrationBuilder.AddColumn<string>(
                name: "AssignmentStateId",
                table: "Delivery",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_AssignmentStateId",
                table: "Delivery",
                column: "AssignmentStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Assignment_AssignmentStateId",
                table: "Delivery",
                column: "AssignmentStateId",
                principalTable: "Assignment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Assignment_AssignmentStateId",
                table: "Delivery");

            migrationBuilder.DropIndex(
                name: "IX_Delivery_AssignmentStateId",
                table: "Delivery");

            migrationBuilder.DropColumn(
                name: "AssignmentStateId",
                table: "Delivery");

            migrationBuilder.AlterColumn<string>(
                name: "AssignmentCode",
                table: "Delivery",
                type: "nvarchar(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_AssignmentCode",
                table: "Delivery",
                column: "AssignmentCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Assignment_AssignmentCode",
                table: "Delivery",
                column: "AssignmentCode",
                principalTable: "Assignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
