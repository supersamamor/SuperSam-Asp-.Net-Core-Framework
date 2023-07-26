using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.DPI.Infrastructure.Migrations
{
    public partial class AddIsActiveOnReportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComparisonOperator",
                table: "ReportQueryFilter");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComparisonOperator",
                table: "ReportQueryFilter",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
