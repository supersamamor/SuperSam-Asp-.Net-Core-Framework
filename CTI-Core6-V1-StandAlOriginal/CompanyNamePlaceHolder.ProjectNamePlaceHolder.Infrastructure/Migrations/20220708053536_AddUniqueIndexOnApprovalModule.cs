using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations
{
    public partial class AddUniqueIndexOnApprovalModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApproverAssignment_ApproverSetupId",
                table: "ApproverAssignment");

            migrationBuilder.AddColumn<string>(
                name: "DataId",
                table: "Approval",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailSendingRemarks",
                table: "Approval",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailSendingStatus",
                table: "Approval",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverSetup_TableName_Entity",
                table: "ApproverSetup",
                columns: new[] { "TableName", "Entity" },
                unique: true,
                filter: "[Entity] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignment_ApproverSetupId_ApproverUserId",
                table: "ApproverAssignment",
                columns: new[] { "ApproverSetupId", "ApproverUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Approval_ApproverUserId",
                table: "Approval",
                column: "ApproverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_DataId",
                table: "Approval",
                column: "DataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApproverSetup_TableName_Entity",
                table: "ApproverSetup");

            migrationBuilder.DropIndex(
                name: "IX_ApproverAssignment_ApproverSetupId_ApproverUserId",
                table: "ApproverAssignment");

            migrationBuilder.DropIndex(
                name: "IX_Approval_ApproverUserId",
                table: "Approval");

            migrationBuilder.DropIndex(
                name: "IX_Approval_DataId",
                table: "Approval");

            migrationBuilder.DropColumn(
                name: "DataId",
                table: "Approval");

            migrationBuilder.DropColumn(
                name: "EmailSendingRemarks",
                table: "Approval");

            migrationBuilder.DropColumn(
                name: "EmailSendingStatus",
                table: "Approval");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignment_ApproverSetupId",
                table: "ApproverAssignment",
                column: "ApproverSetupId");
        }
    }
}
