using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
{
    public partial class RemoveUnusedFieldEmailedOnPaymentTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Emailed",
                table: "PaymentTransaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Emailed",
                table: "PaymentTransaction",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
