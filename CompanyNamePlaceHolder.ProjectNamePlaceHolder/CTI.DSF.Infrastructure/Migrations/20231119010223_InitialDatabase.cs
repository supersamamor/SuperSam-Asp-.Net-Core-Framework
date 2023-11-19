using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.DSF.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
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
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    CompanyCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Active = table.Column<bool>(type: "bit", maxLength: 450, nullable: false),
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    HolidayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HolidayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
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
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
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
                name: "TaskMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TaskNo = table.Column<int>(type: "int", nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TaskClassification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDueDay = table.Column<int>(type: "int", nullable: true),
                    TargetDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HolidayTag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", maxLength: 450, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadProcessor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Module = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UploadType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExceptionFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadProcessor", x => x.Id);
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
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    CompanyCode = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", maxLength: 450, nullable: false),
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
                name: "TaskTag",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TaskMasterId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    TagId = table.Column<string>(type: "nvarchar(36)", nullable: false),
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
                        name: "FK_TaskTag_TaskMaster_TaskMasterId",
                        column: x => x.TaskMasterId,
                        principalTable: "TaskMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UploadStaging",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UploadProcessorId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProcessedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadStaging", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadStaging_UploadProcessor_UploadProcessorId",
                        column: x => x.UploadProcessorId,
                        principalTable: "UploadProcessor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    SectionCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    SectionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", maxLength: 450, nullable: false),
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
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    SectionCode = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    TeamCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", maxLength: 450, nullable: false),
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
                name: "TaskCompanyAssignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TaskMasterId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    SectionId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    TeamId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCompanyAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskCompanyAssignment_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskCompanyAssignment_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskCompanyAssignment_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskCompanyAssignment_TaskMaster_TaskMasterId",
                        column: x => x.TaskMasterId,
                        principalTable: "TaskMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskCompanyAssignment_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    AssignmentCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    TaskCompanyAssignmentId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    PrimaryAssignee = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AlternateAssignee = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
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
                        name: "FK_Assignment_TaskCompanyAssignment_TaskCompanyAssignmentId",
                        column: x => x.TaskCompanyAssignmentId,
                        principalTable: "TaskCompanyAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskApprover",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ApproverUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    TaskCompanyAssignmentId = table.Column<string>(type: "nvarchar(36)", nullable: false),
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
                        name: "FK_TaskApprover_TaskCompanyAssignment_TaskCompanyAssignmentId",
                        column: x => x.TaskCompanyAssignmentId,
                        principalTable: "TaskCompanyAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TaskCompanyAssignmentId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    DeliveryCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AssignmentId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeliveryAttachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HolidayTag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    RejectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CancelledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelledBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Delivery_TaskCompanyAssignment_TaskCompanyAssignmentId",
                        column: x => x.TaskCompanyAssignmentId,
                        principalTable: "TaskCompanyAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryApprovalHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    DeliveryId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    TransactionDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryApprovalHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryApprovalHistory_Delivery_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Delivery",
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
                name: "IX_Assignment_TaskCompanyAssignmentId",
                table: "Assignment",
                column: "TaskCompanyAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Id",
                table: "AuditLogs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_PrimaryKey",
                table: "AuditLogs",
                column: "PrimaryKey");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CompanyCode",
                table: "Company",
                column: "CompanyCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_CompanyName",
                table: "Company",
                column: "CompanyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_CreatedBy",
                table: "Company",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Entity",
                table: "Company",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Id",
                table: "Company",
                column: "Id");

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
                name: "IX_Delivery_TaskCompanyAssignmentId",
                table: "Delivery",
                column: "TaskCompanyAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryApprovalHistory_CreatedBy",
                table: "DeliveryApprovalHistory",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryApprovalHistory_DeliveryId",
                table: "DeliveryApprovalHistory",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryApprovalHistory_Entity",
                table: "DeliveryApprovalHistory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryApprovalHistory_Id",
                table: "DeliveryApprovalHistory",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryApprovalHistory_LastModifiedBy",
                table: "DeliveryApprovalHistory",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryApprovalHistory_LastModifiedDate",
                table: "DeliveryApprovalHistory",
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
                name: "IX_Department_Id",
                table: "Department",
                column: "Id");

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_Id",
                table: "Holiday",
                column: "Id");

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
                name: "IX_Section_Id",
                table: "Section",
                column: "Id");

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
                name: "IX_Tags_Id",
                table: "Tags",
                column: "Id");

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
                name: "IX_TaskApprover_Id",
                table: "TaskApprover",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaskApprover_LastModifiedBy",
                table: "TaskApprover",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskApprover_LastModifiedDate",
                table: "TaskApprover",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_TaskApprover_TaskCompanyAssignmentId",
                table: "TaskApprover",
                column: "TaskCompanyAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCompanyAssignment_CompanyId",
                table: "TaskCompanyAssignment",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCompanyAssignment_CreatedBy",
                table: "TaskCompanyAssignment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCompanyAssignment_DepartmentId",
                table: "TaskCompanyAssignment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCompanyAssignment_Entity",
                table: "TaskCompanyAssignment",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCompanyAssignment_Id",
                table: "TaskCompanyAssignment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCompanyAssignment_LastModifiedBy",
                table: "TaskCompanyAssignment",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCompanyAssignment_LastModifiedDate",
                table: "TaskCompanyAssignment",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCompanyAssignment_SectionId",
                table: "TaskCompanyAssignment",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCompanyAssignment_TaskMasterId",
                table: "TaskCompanyAssignment",
                column: "TaskMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCompanyAssignment_TeamId",
                table: "TaskCompanyAssignment",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaster_CreatedBy",
                table: "TaskMaster",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaster_Entity",
                table: "TaskMaster",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaster_Id",
                table: "TaskMaster",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaster_LastModifiedBy",
                table: "TaskMaster",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaster_LastModifiedDate",
                table: "TaskMaster",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTag_CreatedBy",
                table: "TaskTag",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTag_Entity",
                table: "TaskTag",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTag_Id",
                table: "TaskTag",
                column: "Id");

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
                name: "IX_TaskTag_TaskMasterId",
                table: "TaskTag",
                column: "TaskMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_CreatedBy",
                table: "Team",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Entity",
                table: "Team",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Id",
                table: "Team",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_UploadProcessor_CreatedBy",
                table: "UploadProcessor",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UploadProcessor_Entity",
                table: "UploadProcessor",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_UploadProcessor_Id",
                table: "UploadProcessor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UploadProcessor_LastModifiedBy",
                table: "UploadProcessor",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UploadProcessor_LastModifiedDate",
                table: "UploadProcessor",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_UploadStaging_CreatedBy",
                table: "UploadStaging",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UploadStaging_Entity",
                table: "UploadStaging",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_UploadStaging_Id",
                table: "UploadStaging",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UploadStaging_LastModifiedBy",
                table: "UploadStaging",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UploadStaging_LastModifiedDate",
                table: "UploadStaging",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_UploadStaging_UploadProcessorId",
                table: "UploadStaging",
                column: "UploadProcessorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approval");

            migrationBuilder.DropTable(
                name: "ApproverAssignment");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "DeliveryApprovalHistory");

            migrationBuilder.DropTable(
                name: "Holiday");

            migrationBuilder.DropTable(
                name: "ReportQueryFilter");

            migrationBuilder.DropTable(
                name: "ReportRoleAssignment");

            migrationBuilder.DropTable(
                name: "TaskApprover");

            migrationBuilder.DropTable(
                name: "TaskTag");

            migrationBuilder.DropTable(
                name: "UploadStaging");

            migrationBuilder.DropTable(
                name: "ApprovalRecord");

            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "UploadProcessor");

            migrationBuilder.DropTable(
                name: "ApproverSetup");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "TaskCompanyAssignment");

            migrationBuilder.DropTable(
                name: "TaskMaster");

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
