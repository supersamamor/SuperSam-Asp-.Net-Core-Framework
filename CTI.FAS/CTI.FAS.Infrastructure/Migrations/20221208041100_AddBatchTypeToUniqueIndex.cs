using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
{
    public partial class AddBatchTypeToUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EnrollmentBatch_Date_Batch",
                table: "EnrollmentBatch");

            migrationBuilder.DropIndex(
                name: "IX_Batch_Date_Batch",
                table: "Batch");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentBatch_Date_Batch_BatchStatusType",
                table: "EnrollmentBatch",
                columns: new[] { "Date", "Batch", "BatchStatusType" },
                unique: true,
                filter: "[BatchStatusType] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_Date_Batch_BatchStatusType",
                table: "Batch",
                columns: new[] { "Date", "Batch", "BatchStatusType" },
                unique: true,
                filter: "[BatchStatusType] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EnrollmentBatch_Date_Batch_BatchStatusType",
                table: "EnrollmentBatch");

            migrationBuilder.DropIndex(
                name: "IX_Batch_Date_Batch_BatchStatusType",
                table: "Batch");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentBatch_Date_Batch",
                table: "EnrollmentBatch",
                columns: new[] { "Date", "Batch" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Batch_Date_Batch",
                table: "Batch",
                columns: new[] { "Date", "Batch" },
                unique: true);
        }
    }
}
