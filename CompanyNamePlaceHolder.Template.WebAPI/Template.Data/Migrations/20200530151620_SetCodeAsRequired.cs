using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Data.Migrations
{
    public partial class SetCodeAsRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Template_Code",
                table: "Template");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Template",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Template_Code",
                table: "Template",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Template_Code",
                table: "Template");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Template",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Template_Code",
                table: "Template",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }
    }
}
