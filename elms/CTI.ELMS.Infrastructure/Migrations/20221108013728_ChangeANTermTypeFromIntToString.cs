using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.ELMS.Infrastructure.Migrations
{
    public partial class ChangeANTermTypeFromIntToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ANTermTypeID",
                table: "Offering");

            migrationBuilder.AddColumn<string>(
                name: "ANTermType",
                table: "Offering",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ANTermType",
                table: "Offering");

            migrationBuilder.AddColumn<int>(
                name: "ANTermTypeID",
                table: "Offering",
                type: "int",
                nullable: true);
        }
    }
}
