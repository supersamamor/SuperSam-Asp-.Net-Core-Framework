using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations.Identity
{
    public partial class AddPplusUserField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PplusId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PplusId",
                table: "AspNetUsers",
                column: "PplusId",
                unique: true,
                filter: "[PplusId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PplusId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PplusId",
                table: "AspNetUsers");
        }
    }
}
