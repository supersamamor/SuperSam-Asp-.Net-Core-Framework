using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.DSF.Infrastructure.Migrations
{
    public partial class AddRemarksandTaskDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Delivery",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskDescription",
                table: "Delivery",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Delivery");

            migrationBuilder.DropColumn(
                name: "TaskDescription",
                table: "Delivery");
        }
    }
}
