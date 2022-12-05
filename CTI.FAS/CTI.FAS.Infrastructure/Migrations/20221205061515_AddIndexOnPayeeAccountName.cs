using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
{
    public partial class AddIndexOnPayeeAccountName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Creditor_PayeeAccountName",
                table: "Creditor",
                column: "PayeeAccountName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Creditor_PayeeAccountName",
                table: "Creditor");
        }
    }
}
