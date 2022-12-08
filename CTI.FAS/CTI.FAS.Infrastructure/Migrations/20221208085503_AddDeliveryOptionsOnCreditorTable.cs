using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
{
    public partial class AddDeliveryOptionsOnCreditorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryOptions",
                table: "Creditor",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryOptions",
                table: "Creditor");
        }
    }
}
