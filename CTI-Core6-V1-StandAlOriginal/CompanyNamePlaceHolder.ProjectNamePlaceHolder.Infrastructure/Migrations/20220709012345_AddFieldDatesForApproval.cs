using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations
{
    public partial class AddFieldDatesForApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovalRemarks",
                table: "Approval",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailSendingDateTime",
                table: "Approval",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusUpdateDateTime",
                table: "Approval",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalRemarks",
                table: "Approval");

            migrationBuilder.DropColumn(
                name: "EmailSendingDateTime",
                table: "Approval");

            migrationBuilder.DropColumn(
                name: "StatusUpdateDateTime",
                table: "Approval");
        }
    }
}
