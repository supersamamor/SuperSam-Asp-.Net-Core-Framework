using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.TenantSales.Infrastructure.Migrations
{
    public partial class AddAutocalcTotalNetSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AutoCalculatedTotalNetSales",
                table: "TenantPOSSales",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoCalculatedTotalNetSales",
                table: "TenantPOSSales");
        }
    }
}
