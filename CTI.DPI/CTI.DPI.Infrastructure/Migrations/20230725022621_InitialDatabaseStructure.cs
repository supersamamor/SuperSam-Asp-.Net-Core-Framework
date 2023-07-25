using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.DPI.Infrastructure.Migrations
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
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    QueryType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReportOrChartType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDistinct = table.Column<bool>(type: "bit", nullable: false),
                    QueryString = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    Alias = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AggregationOperator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    LogicalOperator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GroupLevel = table.Column<int>(type: "int", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ComparisonOperator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportTable",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    JoinType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    ReportColumnId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TableId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Function = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ArithmeticOperator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportColumnDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportColumnDetail_ReportColumnHeader_ReportColumnId",
                        column: x => x.ReportColumnId,
                        principalTable: "ReportColumnHeader",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportColumnDetail_ReportTable_TableId",
                        column: x => x.TableId,
                        principalTable: "ReportTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportColumnFilter",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportFilterGroupingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LogicalOperator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TableId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ComparisonOperator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsString = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                        name: "FK_ReportColumnFilter_ReportTable_TableId",
                        column: x => x.TableId,
                        principalTable: "ReportTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportTableJoinParameter",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LogicalOperator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    JoinFromTableId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinFromFieldName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                        name: "FK_ReportTableJoinParameter_ReportTable_TableId",
                        column: x => x.TableId,
                        principalTable: "ReportTable",
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
                name: "IX_Report_Entity",
                table: "Report",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Report_LastModifiedDate",
                table: "Report",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ReportName",
                table: "Report",
                column: "ReportName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_Entity",
                table: "ReportColumnDetail",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_LastModifiedDate",
                table: "ReportColumnDetail",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_ReportColumnId",
                table: "ReportColumnDetail",
                column: "ReportColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_TableId",
                table: "ReportColumnDetail",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_Entity",
                table: "ReportColumnFilter",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_LastModifiedDate",
                table: "ReportColumnFilter",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_ReportFilterGroupingId",
                table: "ReportColumnFilter",
                column: "ReportFilterGroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_TableId",
                table: "ReportColumnFilter",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_Entity",
                table: "ReportColumnHeader",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_LastModifiedDate",
                table: "ReportColumnHeader",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_ReportId",
                table: "ReportColumnHeader",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_Entity",
                table: "ReportFilterGrouping",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_LastModifiedDate",
                table: "ReportFilterGrouping",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_ReportId",
                table: "ReportFilterGrouping",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_Entity",
                table: "ReportQueryFilter",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_LastModifiedDate",
                table: "ReportQueryFilter",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_ReportId",
                table: "ReportQueryFilter",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_Entity",
                table: "ReportTable",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_LastModifiedDate",
                table: "ReportTable",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_ReportId",
                table: "ReportTable",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_Entity",
                table: "ReportTableJoinParameter",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_LastModifiedDate",
                table: "ReportTableJoinParameter",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_ReportId",
                table: "ReportTableJoinParameter",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_TableId",
                table: "ReportTableJoinParameter",
                column: "TableId");
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
                name: "ReportColumnDetail");

            migrationBuilder.DropTable(
                name: "ReportColumnFilter");

            migrationBuilder.DropTable(
                name: "ReportQueryFilter");

            migrationBuilder.DropTable(
                name: "ReportTableJoinParameter");

            migrationBuilder.DropTable(
                name: "ApprovalRecord");

            migrationBuilder.DropTable(
                name: "ReportColumnHeader");

            migrationBuilder.DropTable(
                name: "ReportFilterGrouping");

            migrationBuilder.DropTable(
                name: "ReportTable");

            migrationBuilder.DropTable(
                name: "ApproverSetup");

            migrationBuilder.DropTable(
                name: "Report");
        }
    }
}
