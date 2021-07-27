using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations
{
    public partial class addIndexToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Projects_LastModifiedDate",
                table: "Projects",
                column: "LastModifiedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_LastModifiedDate",
                table: "Projects");
        }
    }
}
