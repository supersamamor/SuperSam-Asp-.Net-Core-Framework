using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPL.ProjectPL.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
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
                name: "ContactInformation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ContactDetails = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInformation_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HealthDeclaration",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    IsVaccinated = table.Column<bool>(type: "bit", nullable: true),
                    Vaccine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthDeclaration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthDeclaration_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Id",
                table: "AuditLogs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_PrimaryKey",
                table: "AuditLogs",
                column: "PrimaryKey");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_CreatedBy",
                table: "ContactInformation",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_EmployeeId",
                table: "ContactInformation",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_Entity",
                table: "ContactInformation",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_Id",
                table: "ContactInformation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_LastModifiedBy",
                table: "ContactInformation",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_LastModifiedDate",
                table: "ContactInformation",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CreatedBy",
                table: "Employee",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeCode",
                table: "Employee",
                column: "EmployeeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Entity",
                table: "Employee",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Id",
                table: "Employee",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_LastModifiedBy",
                table: "Employee",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_LastModifiedDate",
                table: "Employee",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_HealthDeclaration_CreatedBy",
                table: "HealthDeclaration",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_HealthDeclaration_EmployeeId",
                table: "HealthDeclaration",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthDeclaration_Entity",
                table: "HealthDeclaration",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_HealthDeclaration_Id",
                table: "HealthDeclaration",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HealthDeclaration_LastModifiedBy",
                table: "HealthDeclaration",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_HealthDeclaration_LastModifiedDate",
                table: "HealthDeclaration",
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
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "ContactInformation");

            migrationBuilder.DropTable(
                name: "HealthDeclaration");

            migrationBuilder.DropTable(
                name: "ReportQueryFilter");

            migrationBuilder.DropTable(
                name: "ReportRoleAssignment");

            migrationBuilder.DropTable(
                name: "UploadStaging");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "UploadProcessor");
        }
    }
}
