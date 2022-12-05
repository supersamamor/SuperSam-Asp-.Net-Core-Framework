using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
{
    public partial class AdditionalFieldsOnBatchTableForEmailSending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EmailDateTime",
                table: "EnrollmentBatch",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailStatus",
                table: "EnrollmentBatch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "EnrollmentBatch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "EnrollmentBatch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "EnrollmentBatch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailDateTime",
                table: "Batch",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailStatus",
                table: "Batch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Batch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Batch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Batch",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailDateTime",
                table: "EnrollmentBatch");

            migrationBuilder.DropColumn(
                name: "EmailStatus",
                table: "EnrollmentBatch");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "EnrollmentBatch");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "EnrollmentBatch");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EnrollmentBatch");

            migrationBuilder.DropColumn(
                name: "EmailDateTime",
                table: "Batch");

            migrationBuilder.DropColumn(
                name: "EmailStatus",
                table: "Batch");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Batch");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Batch");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Batch");
        }
    }
}
