using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.ELMS.Infrastructure.Migrations
{
    public partial class ProjectIdFromOfferingIsRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offering_Project_ProjectID",
                table: "Offering");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectID",
                table: "Offering",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offering_Project_ProjectID",
                table: "Offering",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offering_Project_ProjectID",
                table: "Offering");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectID",
                table: "Offering",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Offering_Project_ProjectID",
                table: "Offering",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "Id");
        }
    }
}
