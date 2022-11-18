using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.ELMS.Infrastructure.Migrations
{
    public partial class RemoveBoardUpFromOfferingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardUp",
                table: "OfferingHistory");

            migrationBuilder.DropColumn(
                name: "BoardUp",
                table: "Offering");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BoardUp",
                table: "OfferingHistory",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BoardUp",
                table: "Offering",
                type: "decimal(18,6)",
                nullable: true);
        }
    }
}
