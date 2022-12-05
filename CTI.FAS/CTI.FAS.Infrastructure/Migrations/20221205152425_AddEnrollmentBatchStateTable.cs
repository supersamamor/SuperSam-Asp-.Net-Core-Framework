using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
{
    public partial class AddEnrollmentBatchStateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnrollmentBatchId",
                table: "EnrolledPayee",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EnrollmentBatch",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Batch = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentBatch", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledPayee_EnrollmentBatchId",
                table: "EnrolledPayee",
                column: "EnrollmentBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_Date_Batch",
                table: "Batch",
                columns: new[] { "Date", "Batch" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentBatch_Date_Batch",
                table: "EnrollmentBatch",
                columns: new[] { "Date", "Batch" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolledPayee_EnrollmentBatch_EnrollmentBatchId",
                table: "EnrolledPayee",
                column: "EnrollmentBatchId",
                principalTable: "EnrollmentBatch",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolledPayee_EnrollmentBatch_EnrollmentBatchId",
                table: "EnrolledPayee");

            migrationBuilder.DropTable(
                name: "EnrollmentBatch");

            migrationBuilder.DropIndex(
                name: "IX_EnrolledPayee_EnrollmentBatchId",
                table: "EnrolledPayee");

            migrationBuilder.DropIndex(
                name: "IX_Batch_Date_Batch",
                table: "Batch");

            migrationBuilder.DropColumn(
                name: "EnrollmentBatchId",
                table: "EnrolledPayee");
        }
    }
}
