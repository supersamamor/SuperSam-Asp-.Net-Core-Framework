using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.DSF.Infrastructure.Migrations
{
    public partial class Initial : Migration
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
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueryType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportOrChartType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDistinct = table.Column<bool>(type: "bit", nullable: false),
                    QueryString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOnDashboard = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOnReportModule = table.Column<bool>(type: "bit", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskList",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskListCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TaskDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TaskType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDueDay = table.Column<int>(type: "int", nullable: false),
                    TargetDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrimaryEndorser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryApprover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternateEndorser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternateApprover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskList", x => x.Id);
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
                name: "ReportColumnHeader",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AggregationOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportColumnHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportColumnHeader_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportFilterGrouping",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LogicalOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupLevel = table.Column<int>(type: "int", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFilterGrouping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportFilterGrouping_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportQueryFilter",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomDropdownValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DropdownTableKeyAndValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DropdownFilter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportQueryFilter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportQueryFilter_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportRoleAssignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportRoleAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportRoleAssignment_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportTable",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportTable_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignmentCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TaskListCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryAsignee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternateAsignee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskListId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignment_TaskList_TaskListId",
                        column: x => x.TaskListId,
                        principalTable: "TaskList",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "ReportColumnDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportColumnId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArithmeticOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    ReportColumnHeaderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReportTableId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportColumnDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportColumnDetail_ReportColumnHeader_ReportColumnHeaderId",
                        column: x => x.ReportColumnHeaderId,
                        principalTable: "ReportColumnHeader",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportColumnDetail_ReportTable_ReportTableId",
                        column: x => x.ReportTableId,
                        principalTable: "ReportTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportColumnFilter",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportFilterGroupingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LogicalOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComparisonOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsString = table.Column<bool>(type: "bit", nullable: false),
                    ReportTableId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportColumnFilter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportColumnFilter_ReportFilterGrouping_ReportFilterGroupingId",
                        column: x => x.ReportFilterGroupingId,
                        principalTable: "ReportFilterGrouping",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportColumnFilter_ReportTable_ReportTableId",
                        column: x => x.ReportTableId,
                        principalTable: "ReportTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportTableJoinParameter",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LogicalOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinFromTableId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinFromFieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ReportTableId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTableJoinParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportTableJoinParameter_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportTableJoinParameter_ReportTable_ReportTableId",
                        column: x => x.ReportTableId,
                        principalTable: "ReportTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliveryCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignmentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryAttachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignmentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delivery_Assignment_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignment",
                        principalColumn: "Id");
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
                name: "IX_Assignment_AssignmentCode",
                table: "Assignment",
                column: "AssignmentCode",
                unique: true,
                filter: "[AssignmentCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_Entity",
                table: "Assignment",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_LastModifiedDate",
                table: "Assignment",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_TaskListId",
                table: "Assignment",
                column: "TaskListId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_PrimaryKey",
                table: "AuditLogs",
                column: "PrimaryKey");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_AssignmentId",
                table: "Delivery",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_DeliveryCode",
                table: "Delivery",
                column: "DeliveryCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Entity",
                table: "Delivery",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_LastModifiedDate",
                table: "Delivery",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_ReportColumnHeaderId",
                table: "ReportColumnDetail",
                column: "ReportColumnHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_ReportTableId",
                table: "ReportColumnDetail",
                column: "ReportTableId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_ReportFilterGroupingId",
                table: "ReportColumnFilter",
                column: "ReportFilterGroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_ReportTableId",
                table: "ReportColumnFilter",
                column: "ReportTableId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_ReportId",
                table: "ReportColumnHeader",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_ReportId",
                table: "ReportFilterGrouping",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_ReportId",
                table: "ReportQueryFilter",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_ReportId",
                table: "ReportRoleAssignment",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_ReportId",
                table: "ReportTable",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_ReportId",
                table: "ReportTableJoinParameter",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_ReportTableId",
                table: "ReportTableJoinParameter",
                column: "ReportTableId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_Entity",
                table: "TaskList",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_LastModifiedDate",
                table: "TaskList",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_TaskListCode",
                table: "TaskList",
                column: "TaskListCode",
                unique: true,
                filter: "[TaskListCode] IS NOT NULL");
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
                name: "Delivery");

            migrationBuilder.DropTable(
                name: "ReportColumnDetail");

            migrationBuilder.DropTable(
                name: "ReportColumnFilter");

            migrationBuilder.DropTable(
                name: "ReportQueryFilter");

            migrationBuilder.DropTable(
                name: "ReportRoleAssignment");

            migrationBuilder.DropTable(
                name: "ReportTableJoinParameter");

            migrationBuilder.DropTable(
                name: "ApprovalRecord");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "ReportColumnHeader");

            migrationBuilder.DropTable(
                name: "ReportFilterGrouping");

            migrationBuilder.DropTable(
                name: "ReportTable");

            migrationBuilder.DropTable(
                name: "ApproverSetup");

            migrationBuilder.DropTable(
                name: "TaskList");

            migrationBuilder.DropTable(
                name: "Report");
        }
    }
}
