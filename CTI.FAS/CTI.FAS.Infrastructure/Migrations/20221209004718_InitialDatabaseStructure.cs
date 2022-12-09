using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
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
                name: "Batch",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Batch = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BatchStatusType = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DatabaseConnectionSetup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DatabaseAndServerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InhouseDatabaseAndServerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SystemConnectionString = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseConnectionSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentBatch",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Batch = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BatchStatusType = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentBatch", x => x.Id);
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
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DatabaseConnectionSetupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    EntityAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EntityAddressSecondLine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EntityDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EntityShortName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    TINNo = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: true),
                    SubmitPlace = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SubmitDeadline = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    EmailTelephoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImageLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BankCode = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    DeliveryCorporationBranch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SignatoryType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Signatory1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Signatory2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_DatabaseConnectionSetup_DatabaseConnectionSetupId",
                        column: x => x.DatabaseConnectionSetupId,
                        principalTable: "DatabaseConnectionSetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Creditor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreditorAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PayeeAccountName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PayeeAccountLongDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PayeeAccountCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PayeeAccountTIN = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PayeeAccountAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    DeliveryOptions = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DatabaseConnectionSetupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creditor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Creditor_DatabaseConnectionSetup_DatabaseConnectionSetupId",
                        column: x => x.DatabaseConnectionSetupId,
                        principalTable: "DatabaseConnectionSetup",
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
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProjectAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LandArea = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    GLA = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PplusUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEntity_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrolledPayee",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreditorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PayeeAccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PayeeAccountType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EnrollmentBatchId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolledPayee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolledPayee_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrolledPayee_Creditor_CreditorId",
                        column: x => x.CreditorId,
                        principalTable: "Creditor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrolledPayee_EnrollmentBatch_EnrollmentBatchId",
                        column: x => x.EnrollmentBatchId,
                        principalTable: "EnrollmentBatch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenant_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrolledPayeeEmail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    EnrolledPayeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolledPayeeEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolledPayeeEmail_EnrolledPayee_EnrolledPayeeId",
                        column: x => x.EnrolledPayeeId,
                        principalTable: "EnrolledPayee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransaction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EnrolledPayeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BatchId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TransmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    DocumentDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CheckNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PdfUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PdfFilePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IfcaBatchNumber = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    IfcaLineNumber = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    EmailSentCount = table.Column<int>(type: "int", nullable: false),
                    EmailSentDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsForSending = table.Column<bool>(type: "bit", nullable: false),
                    AccountTransaction = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EmailSendingError = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransaction_Batch_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentTransaction_EnrolledPayee_EnrolledPayeeId",
                        column: x => x.EnrolledPayeeId,
                        principalTable: "EnrolledPayee",
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
                name: "IX_Batch_BatchStatusType",
                table: "Batch",
                column: "BatchStatusType");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_CompanyId",
                table: "Batch",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_Date_Batch_BatchStatusType",
                table: "Batch",
                columns: new[] { "Date", "Batch", "BatchStatusType" },
                unique: true,
                filter: "[BatchStatusType] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_Entity",
                table: "Batch",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_LastModifiedDate",
                table: "Batch",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Company_DatabaseConnectionSetupId",
                table: "Company",
                column: "DatabaseConnectionSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Entity",
                table: "Company",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Company_LastModifiedDate",
                table: "Company",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Creditor_CreditorAccount",
                table: "Creditor",
                column: "CreditorAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Creditor_DatabaseConnectionSetupId",
                table: "Creditor",
                column: "DatabaseConnectionSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_Creditor_Entity",
                table: "Creditor",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Creditor_LastModifiedDate",
                table: "Creditor",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Creditor_PayeeAccountName",
                table: "Creditor",
                column: "PayeeAccountName");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseConnectionSetup_Code",
                table: "DatabaseConnectionSetup",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseConnectionSetup_Entity",
                table: "DatabaseConnectionSetup",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseConnectionSetup_LastModifiedDate",
                table: "DatabaseConnectionSetup",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseConnectionSetup_Name",
                table: "DatabaseConnectionSetup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledPayee_CompanyId_CreditorId",
                table: "EnrolledPayee",
                columns: new[] { "CompanyId", "CreditorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledPayee_CreditorId",
                table: "EnrolledPayee",
                column: "CreditorId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledPayee_EnrollmentBatchId",
                table: "EnrolledPayee",
                column: "EnrollmentBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledPayee_Entity",
                table: "EnrolledPayee",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledPayee_LastModifiedDate",
                table: "EnrolledPayee",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledPayeeEmail_EnrolledPayeeId",
                table: "EnrolledPayeeEmail",
                column: "EnrolledPayeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledPayeeEmail_Entity",
                table: "EnrolledPayeeEmail",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledPayeeEmail_LastModifiedDate",
                table: "EnrolledPayeeEmail",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentBatch_BatchStatusType",
                table: "EnrollmentBatch",
                column: "BatchStatusType");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentBatch_CompanyId",
                table: "EnrollmentBatch",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentBatch_Date_Batch_BatchStatusType",
                table: "EnrollmentBatch",
                columns: new[] { "Date", "Batch", "BatchStatusType" },
                unique: true,
                filter: "[BatchStatusType] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_BatchId",
                table: "PaymentTransaction",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_EnrolledPayeeId",
                table: "PaymentTransaction",
                column: "EnrolledPayeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_Entity",
                table: "PaymentTransaction",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_IfcaBatchNumber",
                table: "PaymentTransaction",
                column: "IfcaBatchNumber");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_IfcaLineNumber",
                table: "PaymentTransaction",
                column: "IfcaLineNumber");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_LastModifiedDate",
                table: "PaymentTransaction",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CompanyId",
                table: "Project",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Entity",
                table: "Project",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Project_LastModifiedDate",
                table: "Project",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Entity",
                table: "Tenant",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_LastModifiedDate",
                table: "Tenant",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_ProjectId",
                table: "Tenant",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntity_CompanyId",
                table: "UserEntity",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntity_Entity",
                table: "UserEntity",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntity_LastModifiedDate",
                table: "UserEntity",
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
                name: "EnrolledPayeeEmail");

            migrationBuilder.DropTable(
                name: "PaymentTransaction");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropTable(
                name: "UserEntity");

            migrationBuilder.DropTable(
                name: "ApprovalRecord");

            migrationBuilder.DropTable(
                name: "Batch");

            migrationBuilder.DropTable(
                name: "EnrolledPayee");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "ApproverSetup");

            migrationBuilder.DropTable(
                name: "Creditor");

            migrationBuilder.DropTable(
                name: "EnrollmentBatch");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "DatabaseConnectionSetup");
        }
    }
}
