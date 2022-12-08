using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
{
    public partial class AddBatchStatusType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BatchStatusType",
                table: "EnrollmentBatch",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BatchStatusType",
                table: "Batch",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentBatch_BatchStatusType",
                table: "EnrollmentBatch",
                column: "BatchStatusType");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_BatchStatusType",
                table: "Batch",
                column: "BatchStatusType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EnrollmentBatch_BatchStatusType",
                table: "EnrollmentBatch");

            migrationBuilder.DropIndex(
                name: "IX_Batch_BatchStatusType",
                table: "Batch");

            migrationBuilder.DropColumn(
                name: "BatchStatusType",
                table: "EnrollmentBatch");

            migrationBuilder.DropColumn(
                name: "BatchStatusType",
                table: "Batch");
        }
    }
}
