using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
{
    public partial class AddFieldAccountTransactionOnPaymentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountTransaction",
                table: "PaymentTransaction",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountTransaction",
                table: "PaymentTransaction");
        }
    }
}
