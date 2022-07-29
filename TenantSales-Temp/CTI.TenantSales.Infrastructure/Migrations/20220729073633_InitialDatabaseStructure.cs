using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.TenantSales.Infrastructure.Migrations
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
                    TableName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
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
                name: "BusinessUnit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUnit", x => x.Id);
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
                    SystemSource = table.Column<int>(type: "int", nullable: false),
                    ExhibitThemeCodes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
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
                name: "RentalType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.Id);
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
                    ApproverSetupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ApproverUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
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
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    EntityAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EntityAddressSecondLine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EntityDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EntityShortName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    TINNo = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: true),
                    DatabaseConnectionSetupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "Classification",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ThemeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classification_Theme_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Theme",
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
                    PayableTo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProjectAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProjectNameANSection = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LandArea = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    GLA = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    SignatoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SignatoryPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ANSignatoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ANSignatoryPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContractSignatory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContractSignatoryPosition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContractSignatoryProofOfIdentity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContractSignatoryWitness = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContractSignatoryWitnessPosition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OutsideFC = table.Column<bool>(type: "bit", nullable: false),
                    ProjectGreetingsSection = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProjectShortName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SignatureUpper = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SignatureLower = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HasAssociationDues = table.Column<bool>(type: "bit", nullable: false),
                    SalesUploadFolder = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EnableMeterReadingApp = table.Column<bool>(type: "bit", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CurrencyRate = table.Column<int>(type: "int", nullable: true),
                    GasCutOff = table.Column<int>(type: "int", nullable: true),
                    PowerCutOff = table.Column<int>(type: "int", nullable: true),
                    WaterCutOff = table.Column<int>(type: "int", nullable: true),
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
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ClassificationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Classification_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Classification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HasPercentageSalesTenant = table.Column<bool>(type: "bit", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Level_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectBusinessUnit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BusinessUnitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectBusinessUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectBusinessUnit_BusinessUnit_BusinessUnitId",
                        column: x => x.BusinessUnitId,
                        principalTable: "BusinessUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectBusinessUnit_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RentalTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FileCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Folder = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Opening = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LevelId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    BranchContact = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HeadOfficeContact = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ITSupportContact = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                        name: "FK_Tenant_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tenant_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tenant_RentalType_RentalTypeId",
                        column: x => x.RentalTypeId,
                        principalTable: "RentalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesCategory_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenantContact",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Group = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantContact_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenantLot",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Area = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    LotNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantLot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantLot_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenantPOS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantPOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantPOS_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenantPOSSales",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SalesType = table.Column<int>(type: "int", nullable: false),
                    HourCode = table.Column<int>(type: "int", nullable: false),
                    SalesCategory = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SalesDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAutoCompute = table.Column<bool>(type: "bit", nullable: false),
                    SalesAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    OldAccumulatedTotal = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    NewAccumulatedTotal = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TaxableSalesAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    NonTaxableSalesAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    SeniorCitizenDiscount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PromoDiscount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    OtherDiscount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    RefundDiscount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    VoidAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    AdjustmentAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TotalServiceCharge = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    NoOfSalesTransactions = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    NoOfTransactions = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TotalNetSales = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    ControlNumber = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TenantPOSId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidationStatus = table.Column<int>(type: "int", nullable: false),
                    ValidationRemarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AutocalculatedNewAccumulatedTotal = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    AutocalculatedOldAccumulatedTotal = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantPOSSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantPOSSales_TenantPOS_TenantPOSId",
                        column: x => x.TenantPOSId,
                        principalTable: "TenantPOS",
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
                name: "IX_ApproverAssignment_ApproverSetupId_ApproverUserId",
                table: "ApproverAssignment",
                columns: new[] { "ApproverSetupId", "ApproverUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApproverSetup_TableName_Entity",
                table: "ApproverSetup",
                columns: new[] { "TableName", "Entity" },
                unique: true,
                filter: "[Entity] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_PrimaryKey",
                table: "AuditLogs",
                column: "PrimaryKey");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUnit_Entity",
                table: "BusinessUnit",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUnit_LastModifiedDate",
                table: "BusinessUnit",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUnit_Name",
                table: "BusinessUnit",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_ClassificationId",
                table: "Category",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Entity",
                table: "Category",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Category_LastModifiedDate",
                table: "Category",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Classification_Entity",
                table: "Classification",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Classification_LastModifiedDate",
                table: "Classification",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Classification_ThemeId",
                table: "Classification",
                column: "ThemeId");

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
                name: "IX_Level_Entity",
                table: "Level",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Level_LastModifiedDate",
                table: "Level",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Level_ProjectId",
                table: "Level",
                column: "ProjectId");

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
                name: "IX_ProjectBusinessUnit_BusinessUnitId",
                table: "ProjectBusinessUnit",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBusinessUnit_Entity",
                table: "ProjectBusinessUnit",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBusinessUnit_LastModifiedDate",
                table: "ProjectBusinessUnit",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBusinessUnit_ProjectId",
                table: "ProjectBusinessUnit",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalType_Entity",
                table: "RentalType",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_RentalType_LastModifiedDate",
                table: "RentalType",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_RentalType_Name",
                table: "RentalType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesCategory_Entity",
                table: "SalesCategory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_SalesCategory_LastModifiedDate",
                table: "SalesCategory",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_SalesCategory_TenantId",
                table: "SalesCategory",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Entity",
                table: "Tenant",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_LastModifiedDate",
                table: "Tenant",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_LevelId",
                table: "Tenant",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_ProjectId",
                table: "Tenant",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_RentalTypeId",
                table: "Tenant",
                column: "RentalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantContact_Entity",
                table: "TenantContact",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_TenantContact_LastModifiedDate",
                table: "TenantContact",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_TenantContact_TenantId",
                table: "TenantContact",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantLot_Entity",
                table: "TenantLot",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_TenantLot_LastModifiedDate",
                table: "TenantLot",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_TenantLot_TenantId",
                table: "TenantLot",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPOS_Entity",
                table: "TenantPOS",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPOS_LastModifiedDate",
                table: "TenantPOS",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPOS_TenantId",
                table: "TenantPOS",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPOSSales_Entity",
                table: "TenantPOSSales",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPOSSales_LastModifiedDate",
                table: "TenantPOSSales",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPOSSales_TenantPOSId",
                table: "TenantPOSSales",
                column: "TenantPOSId");

            migrationBuilder.CreateIndex(
                name: "IX_Theme_Code",
                table: "Theme",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Theme_Entity",
                table: "Theme",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Theme_LastModifiedDate",
                table: "Theme",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Theme_Name",
                table: "Theme",
                column: "Name",
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
                name: "Category");

            migrationBuilder.DropTable(
                name: "ProjectBusinessUnit");

            migrationBuilder.DropTable(
                name: "SalesCategory");

            migrationBuilder.DropTable(
                name: "TenantContact");

            migrationBuilder.DropTable(
                name: "TenantLot");

            migrationBuilder.DropTable(
                name: "TenantPOSSales");

            migrationBuilder.DropTable(
                name: "ApprovalRecord");

            migrationBuilder.DropTable(
                name: "Classification");

            migrationBuilder.DropTable(
                name: "BusinessUnit");

            migrationBuilder.DropTable(
                name: "TenantPOS");

            migrationBuilder.DropTable(
                name: "ApproverSetup");

            migrationBuilder.DropTable(
                name: "Theme");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "RentalType");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "DatabaseConnectionSetup");
        }
    }
}
