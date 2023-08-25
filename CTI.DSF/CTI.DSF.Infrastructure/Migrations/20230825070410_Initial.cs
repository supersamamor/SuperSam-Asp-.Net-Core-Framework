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
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holiday",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HolidayDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HolidayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.Id);
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
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalRecord",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApproverSetupId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    DataId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApproverType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproverSetupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ApproverUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ApproverRoleId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
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
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Company_CompanyCode",
                        column: x => x.CompanyCode,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportColumnHeader",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    SectionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Section_Department_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportFilterGroupingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LogicalOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComparisonOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsString = table.Column<bool>(type: "bit", nullable: false),
                    ReportTableId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LogicalOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinFromTableId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinFromFieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ReportTableId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeamCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Section_SectionCode",
                        column: x => x.SectionCode,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskList",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskListNo = table.Column<int>(type: "int", nullable: true),
                    TaskDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TaskClassification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskFrequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskDueDay = table.Column<int>(type: "int", nullable: true),
                    TargetDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HolidayTag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SectionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TeamId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskList_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskList_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskList_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskList_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignmentCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    TaskListId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrimaryAssignee = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    AlternateAssignee = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                        name: "FK_Assignment_TaskList_TaskListId",
                        column: x => x.TaskListId,
                        principalTable: "TaskList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskApprover",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApproverUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TaskListId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApproverType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskApprover", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskApprover_TaskList_TaskListId",
                        column: x => x.TaskListId,
                        principalTable: "TaskList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskTag",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskListId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTag_TaskList_TaskListId",
                        column: x => x.TaskListId,
                        principalTable: "TaskList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliveryCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    AssignmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAttachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HolidayTag = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        name: "FK_Delivery_Assignment_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignment",
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
                name: "IX_Assignment_CreatedBy",
                table: "Assignment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_Entity",
                table: "Assignment",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_LastModifiedBy",
                table: "Assignment",
                column: "LastModifiedBy");

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
                name: "IX_Company_CompanyCode",
                table: "Company",
                column: "CompanyCode",
                unique: true,
                filter: "[CompanyCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CompanyName",
                table: "Company",
                column: "CompanyName",
                unique: true,
                filter: "[CompanyName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CreatedBy",
                table: "Company",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Entity",
                table: "Company",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Company_LastModifiedBy",
                table: "Company",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Company_LastModifiedDate",
                table: "Company",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_AssignmentId",
                table: "Delivery",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_CreatedBy",
                table: "Delivery",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Entity",
                table: "Delivery",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_LastModifiedBy",
                table: "Delivery",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_LastModifiedDate",
                table: "Delivery",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CompanyCode",
                table: "Department",
                column: "CompanyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CreatedBy",
                table: "Department",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Entity",
                table: "Department",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Department_LastModifiedBy",
                table: "Department",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Department_LastModifiedDate",
                table: "Department",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_CreatedBy",
                table: "Holiday",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_Entity",
                table: "Holiday",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_HolidayName",
                table: "Holiday",
                column: "HolidayName",
                unique: true,
                filter: "[HolidayName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_LastModifiedBy",
                table: "Holiday",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_LastModifiedDate",
                table: "Holiday",
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
                name: "IX_Section_CreatedBy",
                table: "Section",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Section_DepartmentCode",
                table: "Section",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Section_Entity",
                table: "Section",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Section_LastModifiedBy",
                table: "Section",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Section_LastModifiedDate",
                table: "Section",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CreatedBy",
                table: "Tags",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Entity",
                table: "Tags",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_LastModifiedBy",
                table: "Tags",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_LastModifiedDate",
                table: "Tags",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskApprover_CreatedBy",
                table: "TaskApprover",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskApprover_Entity",
                table: "TaskApprover",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_TaskApprover_LastModifiedBy",
                table: "TaskApprover",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskApprover_LastModifiedDate",
                table: "TaskApprover",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_TaskApprover_TaskListId",
                table: "TaskApprover",
                column: "TaskListId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_CompanyId",
                table: "TaskList",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_CreatedBy",
                table: "TaskList",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_DepartmentId",
                table: "TaskList",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_Entity",
                table: "TaskList",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_LastModifiedBy",
                table: "TaskList",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_LastModifiedDate",
                table: "TaskList",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_SectionId",
                table: "TaskList",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_TeamId",
                table: "TaskList",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTag_CreatedBy",
                table: "TaskTag",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTag_Entity",
                table: "TaskTag",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTag_LastModifiedBy",
                table: "TaskTag",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTag_LastModifiedDate",
                table: "TaskTag",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTag_TagId",
                table: "TaskTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTag_TaskListId",
                table: "TaskTag",
                column: "TaskListId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_CreatedBy",
                table: "Team",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Entity",
                table: "Team",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Team_LastModifiedBy",
                table: "Team",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Team_LastModifiedDate",
                table: "Team",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Team_SectionCode",
                table: "Team",
                column: "SectionCode");
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
                name: "Holiday");

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
                name: "TaskApprover");

            migrationBuilder.DropTable(
                name: "TaskTag");

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
                name: "Tags");

            migrationBuilder.DropTable(
                name: "ApproverSetup");

            migrationBuilder.DropTable(
                name: "TaskList");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
