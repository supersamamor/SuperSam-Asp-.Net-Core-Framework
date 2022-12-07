using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
{
    public partial class AddCompanyIdOnBatchTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "EnrollmentBatch",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "Batch",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentBatch_CompanyId",
                table: "EnrollmentBatch",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_CompanyId",
                table: "Batch",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EnrollmentBatch_CompanyId",
                table: "EnrollmentBatch");

            migrationBuilder.DropIndex(
                name: "IX_Batch_CompanyId",
                table: "Batch");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "EnrollmentBatch");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Batch");
        }
    }
}
