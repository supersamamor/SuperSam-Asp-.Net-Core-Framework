using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.ELMS.Infrastructure.Migrations
{
    public partial class AddHasAnnualIncrementFromOfferingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasAnnualIncrement",
                table: "UnitOffered",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAnnualIncrement",
                table: "UnitOffered");
        }
    }
}
