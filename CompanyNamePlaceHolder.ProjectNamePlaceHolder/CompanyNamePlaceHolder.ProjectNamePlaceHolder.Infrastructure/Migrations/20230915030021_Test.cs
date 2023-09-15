using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApproverSetup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ApprovalSetupType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkflowName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    WorkflowDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ApprovalType = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    EmailBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<int>(type: "int", maxLength: 36, nullable: false)
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueryType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportOrChartType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDistinct = table.Column<bool>(type: "bit", nullable: false),
                    QueryString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOnDashboard = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOnReportModule = table.Column<bool>(type: "bit", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TaskListCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TaskType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDueDay = table.Column<int>(type: "int", nullable: false),
                    TargetDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrimaryEndorser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryApprover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternateEndorser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternateApprover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ApproverSetupId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    DataId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ApproverType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproverSetupId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ApproverUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ApproverRoleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AggregationOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    LogicalOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupLevel = table.Column<int>(type: "int", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomDropdownValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DropdownTableKeyAndValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DropdownFilter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    AssignmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskListCode = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    PrimaryAsignee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternateAsignee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignment_TaskList_TaskListCode",
                        column: x => x.TaskListCode,
                        principalTable: "TaskList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Approval",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ApproverUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ApprovalRecordId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    StatusUpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailSendingStatus = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    EmailSendingRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSendingDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReportColumnId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArithmeticOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    ReportColumnHeaderId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    ReportTableId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReportFilterGroupingId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    LogicalOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComparisonOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsString = table.Column<bool>(type: "bit", nullable: false),
                    ReportTableId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    LogicalOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinFromTableId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinFromFieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ReportTableId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ApproverRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndorserRemarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EndorsedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndorsedTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActualDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryAttachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualDeliveryRemarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AssignmentCode = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delivery_Assignment_AssignmentCode",
                        column: x => x.AssignmentCode,
                        principalTable: "Assignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Approval_CreatedBy",
                table: "Approval",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_EmailSendingStatus",
                table: "Approval",
                column: "EmailSendingStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_Entity",
                table: "Approval",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_Id",
                table: "Approval",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_LastModifiedBy",
                table: "Approval",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_LastModifiedDate",
                table: "Approval",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_Status",
                table: "Approval",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_ApproverSetupId",
                table: "ApprovalRecord",
                column: "ApproverSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_CreatedBy",
                table: "ApprovalRecord",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_DataId",
                table: "ApprovalRecord",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_Entity",
                table: "ApprovalRecord",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_Id",
                table: "ApprovalRecord",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_LastModifiedBy",
                table: "ApprovalRecord",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_LastModifiedDate",
                table: "ApprovalRecord",
                column: "LastModifiedDate");

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
                name: "IX_ApproverAssignment_CreatedBy",
                table: "ApproverAssignment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignment_Entity",
                table: "ApproverAssignment",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignment_Id",
                table: "ApproverAssignment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignment_LastModifiedBy",
                table: "ApproverAssignment",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignment_LastModifiedDate",
                table: "ApproverAssignment",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverSetup_CreatedBy",
                table: "ApproverSetup",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverSetup_Entity",
                table: "ApproverSetup",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverSetup_Id",
                table: "ApproverSetup",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverSetup_LastModifiedBy",
                table: "ApproverSetup",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverSetup_LastModifiedDate",
                table: "ApproverSetup",
                column: "LastModifiedDate");

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_CreatedBy",
                table: "Assignment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_Entity",
                table: "Assignment",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_Id",
                table: "Assignment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_LastModifiedBy",
                table: "Assignment",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_LastModifiedDate",
                table: "Assignment",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_TaskListCode",
                table: "Assignment",
                column: "TaskListCode");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Id",
                table: "AuditLogs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_PrimaryKey",
                table: "AuditLogs",
                column: "PrimaryKey");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_AssignmentCode",
                table: "Delivery",
                column: "AssignmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_CreatedBy",
                table: "Delivery",
                column: "CreatedBy");

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
                name: "IX_Delivery_Id",
                table: "Delivery",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_LastModifiedBy",
                table: "Delivery",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_LastModifiedDate",
                table: "Delivery",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Report_CreatedBy",
                table: "Report",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Entity",
                table: "Report",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Id",
                table: "Report",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Report_LastModifiedBy",
                table: "Report",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Report_LastModifiedDate",
                table: "Report",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_CreatedBy",
                table: "ReportColumnDetail",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_Entity",
                table: "ReportColumnDetail",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_Id",
                table: "ReportColumnDetail",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_LastModifiedBy",
                table: "ReportColumnDetail",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_LastModifiedDate",
                table: "ReportColumnDetail",
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
                name: "IX_ReportColumnFilter_CreatedBy",
                table: "ReportColumnFilter",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_Entity",
                table: "ReportColumnFilter",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_Id",
                table: "ReportColumnFilter",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_LastModifiedBy",
                table: "ReportColumnFilter",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_LastModifiedDate",
                table: "ReportColumnFilter",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_ReportFilterGroupingId",
                table: "ReportColumnFilter",
                column: "ReportFilterGroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_ReportTableId",
                table: "ReportColumnFilter",
                column: "ReportTableId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_CreatedBy",
                table: "ReportColumnHeader",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_Entity",
                table: "ReportColumnHeader",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_Id",
                table: "ReportColumnHeader",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_LastModifiedBy",
                table: "ReportColumnHeader",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_LastModifiedDate",
                table: "ReportColumnHeader",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_ReportId",
                table: "ReportColumnHeader",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_CreatedBy",
                table: "ReportFilterGrouping",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_Entity",
                table: "ReportFilterGrouping",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_Id",
                table: "ReportFilterGrouping",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_LastModifiedBy",
                table: "ReportFilterGrouping",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_LastModifiedDate",
                table: "ReportFilterGrouping",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_ReportId",
                table: "ReportFilterGrouping",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_CreatedBy",
                table: "ReportQueryFilter",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_Entity",
                table: "ReportQueryFilter",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_Id",
                table: "ReportQueryFilter",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_LastModifiedBy",
                table: "ReportQueryFilter",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_LastModifiedDate",
                table: "ReportQueryFilter",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_ReportId",
                table: "ReportQueryFilter",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_CreatedBy",
                table: "ReportRoleAssignment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_Entity",
                table: "ReportRoleAssignment",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_Id",
                table: "ReportRoleAssignment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_LastModifiedBy",
                table: "ReportRoleAssignment",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_LastModifiedDate",
                table: "ReportRoleAssignment",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_ReportId",
                table: "ReportRoleAssignment",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_CreatedBy",
                table: "ReportTable",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_Entity",
                table: "ReportTable",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_Id",
                table: "ReportTable",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_LastModifiedBy",
                table: "ReportTable",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_LastModifiedDate",
                table: "ReportTable",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_ReportId",
                table: "ReportTable",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_CreatedBy",
                table: "ReportTableJoinParameter",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_Entity",
                table: "ReportTableJoinParameter",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_Id",
                table: "ReportTableJoinParameter",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_LastModifiedBy",
                table: "ReportTableJoinParameter",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_LastModifiedDate",
                table: "ReportTableJoinParameter",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_ReportId",
                table: "ReportTableJoinParameter",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_ReportTableId",
                table: "ReportTableJoinParameter",
                column: "ReportTableId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_CreatedBy",
                table: "TaskList",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_Entity",
                table: "TaskList",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_Id",
                table: "TaskList",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_LastModifiedBy",
                table: "TaskList",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_LastModifiedDate",
                table: "TaskList",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_TaskListCode",
                table: "TaskList",
                column: "TaskListCode",
                unique: true);
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
