using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNPlaceHolder.PNPlaceHolder.Infrastructure.Migrations
{
    public partial class InitialDatabaseStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApproverSetup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovalSetupType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkflowName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    WorkflowDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ApprovalType = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    EmailBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    TraceId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModPlaceHolder",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ColPlaceHolder = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModPlaceHolder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalRecord",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApproverSetupId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    DataId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalRecord_ApproverSetup_ApproverSetupId",
                        column: x => x.ApproverSetupId,
                        principalTable: "ApproverSetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApproverAssignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApproverType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproverSetupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ApproverUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ApproverRoleId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApproverAssignment_ApproverSetup_ApproverSetupId",
                        column: x => x.ApproverSetupId,
                        principalTable: "ApproverSetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Approval",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApproverUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ApprovalRecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    StatusUpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailSendingStatus = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    EmailSendingRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSendingDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Approval_ApprovalRecord_ApprovalRecordId",
                        column: x => x.ApprovalRecordId,
                        principalTable: "ApprovalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Approval_ApprovalRecordId",
                table: "Approval",
                column: "ApprovalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_ApproverUserId",
                table: "Approval",
                column: "ApproverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_EmailSendingStatus",
                table: "Approval",
                column: "EmailSendingStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_Status",
                table: "Approval",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_ApproverSetupId",
                table: "ApprovalRecord",
                column: "ApproverSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_DataId",
                table: "ApprovalRecord",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_Status",
                table: "ApprovalRecord",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignment_ApproverSetupId_ApproverUserId_ApproverRoleId",
                table: "ApproverAssignment",
                columns: new[] { "ApproverSetupId", "ApproverUserId", "ApproverRoleId" },
                unique: true,
                filter: "[ApproverUserId] IS NOT NULL AND [ApproverRoleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverSetup_WorkflowName_ApprovalSetupType_TableName_Entity",
                table: "ApproverSetup",
                columns: new[] { "WorkflowName", "ApprovalSetupType", "TableName", "Entity" },
                unique: true,
                filter: "[WorkflowName] IS NOT NULL AND [TableName] IS NOT NULL AND [Entity] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_PrimaryKey",
                table: "AuditLogs",
                column: "PrimaryKey");

            migrationBuilder.CreateIndex(
                name: "IX_ModPlaceHolder_ColPlaceHolder",
                table: "ModPlaceHolder",
                column: "ColPlaceHolder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModPlaceHolder_Entity",
                table: "ModPlaceHolder",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ModPlaceHolder_LastModifiedDate",
                table: "ModPlaceHolder",
                column: "LastModifiedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approval");

            migrationBuilder.DropTable(
                name: "ApproverAssignment");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "ModPlaceHolder");

            migrationBuilder.DropTable(
                name: "ApprovalRecord");

            migrationBuilder.DropTable(
                name: "ApproverSetup");
        }
    }
}
