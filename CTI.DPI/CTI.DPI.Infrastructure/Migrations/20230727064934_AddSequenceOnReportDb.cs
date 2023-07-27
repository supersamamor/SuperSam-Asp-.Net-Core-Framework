using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.DPI.Infrastructure.Migrations
{
    public partial class AddSequenceOnReportDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "Report");
        }
    }
}
