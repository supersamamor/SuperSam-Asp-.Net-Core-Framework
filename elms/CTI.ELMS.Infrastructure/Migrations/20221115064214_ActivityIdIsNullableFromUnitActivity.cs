using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.ELMS.Infrastructure.Migrations
{
    public partial class ActivityIdIsNullableFromUnitActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitActivity_Activity_ActivityID",
                table: "UnitActivity");

            migrationBuilder.AlterColumn<string>(
                name: "ActivityID",
                table: "UnitActivity",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitActivity_Activity_ActivityID",
                table: "UnitActivity",
                column: "ActivityID",
                principalTable: "Activity",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitActivity_Activity_ActivityID",
                table: "UnitActivity");

            migrationBuilder.AlterColumn<string>(
                name: "ActivityID",
                table: "UnitActivity",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitActivity_Activity_ActivityID",
                table: "UnitActivity",
                column: "ActivityID",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
