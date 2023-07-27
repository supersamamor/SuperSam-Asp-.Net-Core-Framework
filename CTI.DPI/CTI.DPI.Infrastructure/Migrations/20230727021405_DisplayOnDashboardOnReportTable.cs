using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.DPI.Infrastructure.Migrations
{
    public partial class DisplayOnDashboardOnReportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Report",
                newName: "DisplayOnDashboard");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayOnDashboard",
                table: "Report",
                newName: "IsActive");
        }
    }
}
