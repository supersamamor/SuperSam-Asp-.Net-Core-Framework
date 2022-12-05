using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
{
    public partial class AddIndexOnPaymentTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EnrolledPayee_CompanyId",
                table: "EnrolledPayee");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_IfcaBatchNumber",
                table: "PaymentTransaction",
                column: "IfcaBatchNumber");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_IfcaLineNumber",
                table: "PaymentTransaction",
                column: "IfcaLineNumber");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledPayee_CompanyId_CreditorId",
                table: "EnrolledPayee",
                columns: new[] { "CompanyId", "CreditorId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentTransaction_IfcaBatchNumber",
                table: "PaymentTransaction");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransaction_IfcaLineNumber",
                table: "PaymentTransaction");

            migrationBuilder.DropIndex(
                name: "IX_EnrolledPayee_CompanyId_CreditorId",
                table: "EnrolledPayee");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledPayee_CompanyId",
                table: "EnrolledPayee",
                column: "CompanyId");
        }
    }
}
