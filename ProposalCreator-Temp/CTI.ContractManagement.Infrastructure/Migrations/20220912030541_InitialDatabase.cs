using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.ContractManagement.Infrastructure.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationConfiguration",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLineOne = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    AddressLineTwo = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    OrganizationOverview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentFooter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationConfiguration", x => x.Id);
                });

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
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContactPersonName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContactPersonPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CompanyDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CompanyAddressLineOne = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    CompanyAddressLineTwo = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frequency",
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
                    table.PrimaryKey("PK_Frequency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MilestoneStage",
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
                    table.PrimaryKey("PK_MilestoneStage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PricingType",
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
                    table.PrimaryKey("PK_PricingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCategory",
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
                    table.PrimaryKey("PK_ProjectCategory", x => x.Id);
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
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectGoals = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PricingTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EnablePricing = table.Column<bool>(type: "bit", nullable: false),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RevisionSummary = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    DocumentCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                        name: "FK_Project_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_PricingType_PricingTypeId",
                        column: x => x.PricingTypeId,
                        principalTable: "PricingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deliverable",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectCategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliverable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliverable_ProjectCategory_ProjectCategoryId",
                        column: x => x.ProjectCategoryId,
                        principalTable: "ProjectCategory",
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
                name: "ProjectHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectGoals = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PricingTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EnablePricing = table.Column<bool>(type: "bit", nullable: false),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RevisionSummary = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    DocumentCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectHistory_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHistory_PricingType_PricingTypeId",
                        column: x => x.PricingTypeId,
                        principalTable: "PricingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHistory_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMilestone",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MilestoneStageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    FrequencyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FrequencyQuantity = table.Column<int>(type: "int", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMilestone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMilestone_Frequency_FrequencyId",
                        column: x => x.FrequencyId,
                        principalTable: "Frequency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMilestone_MilestoneStage_MilestoneStageId",
                        column: x => x.MilestoneStageId,
                        principalTable: "MilestoneStage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMilestone_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPackage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OptionNumber = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPackage_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDeliverable",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliverableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDeliverable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDeliverable_Deliverable_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDeliverable_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDeliverableHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectHistoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliverableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDeliverableHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDeliverableHistory_Deliverable_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDeliverableHistory_ProjectHistory_ProjectHistoryId",
                        column: x => x.ProjectHistoryId,
                        principalTable: "ProjectHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMilestoneHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectHistoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MilestoneStageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    FrequencyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FrequencyQuantity = table.Column<int>(type: "int", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMilestoneHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMilestoneHistory_Frequency_FrequencyId",
                        column: x => x.FrequencyId,
                        principalTable: "Frequency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMilestoneHistory_MilestoneStage_MilestoneStageId",
                        column: x => x.MilestoneStageId,
                        principalTable: "MilestoneStage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMilestoneHistory_ProjectHistory_ProjectHistoryId",
                        column: x => x.ProjectHistoryId,
                        principalTable: "ProjectHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPackageHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectHistoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OptionNumber = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPackageHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPackageHistory_ProjectHistory_ProjectHistoryId",
                        column: x => x.ProjectHistoryId,
                        principalTable: "ProjectHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPackageAdditionalDeliverable",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectPackageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliverableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPackageAdditionalDeliverable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPackageAdditionalDeliverable_Deliverable_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPackageAdditionalDeliverable_ProjectPackage_ProjectPackageId",
                        column: x => x.ProjectPackageId,
                        principalTable: "ProjectPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPackageAdditionalDeliverableHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectPackageHistoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliverableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPackageAdditionalDeliverableHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPackageAdditionalDeliverableHistory_Deliverable_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPackageAdditionalDeliverableHistory_ProjectPackageHistory_ProjectPackageHistoryId",
                        column: x => x.ProjectPackageHistoryId,
                        principalTable: "ProjectPackageHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationConfiguration_Entity",
                table: "ApplicationConfiguration",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationConfiguration_LastModifiedDate",
                table: "ApplicationConfiguration",
                column: "LastModifiedDate");

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
                name: "IX_Client_ContactPersonName",
                table: "Client",
                column: "ContactPersonName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_EmailAddress",
                table: "Client",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_Entity",
                table: "Client",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Client_LastModifiedDate",
                table: "Client",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Deliverable_Entity",
                table: "Deliverable",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Deliverable_LastModifiedDate",
                table: "Deliverable",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Deliverable_Name",
                table: "Deliverable",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliverable_ProjectCategoryId",
                table: "Deliverable",
                column: "ProjectCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Frequency_Entity",
                table: "Frequency",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Frequency_LastModifiedDate",
                table: "Frequency",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Frequency_Name",
                table: "Frequency",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneStage_Entity",
                table: "MilestoneStage",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneStage_LastModifiedDate",
                table: "MilestoneStage",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneStage_Name",
                table: "MilestoneStage",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PricingType_Entity",
                table: "PricingType",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_PricingType_LastModifiedDate",
                table: "PricingType",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_PricingType_Name",
                table: "PricingType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_ClientId",
                table: "Project",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_DocumentCode",
                table: "Project",
                column: "DocumentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_Entity",
                table: "Project",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Project_LastModifiedDate",
                table: "Project",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Project_PricingTypeId",
                table: "Project",
                column: "PricingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategory_Entity",
                table: "ProjectCategory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategory_LastModifiedDate",
                table: "ProjectCategory",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategory_Name",
                table: "ProjectCategory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDeliverable_DeliverableId",
                table: "ProjectDeliverable",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDeliverable_Entity",
                table: "ProjectDeliverable",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDeliverable_LastModifiedDate",
                table: "ProjectDeliverable",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDeliverable_ProjectId",
                table: "ProjectDeliverable",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDeliverableHistory_DeliverableId",
                table: "ProjectDeliverableHistory",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDeliverableHistory_Entity",
                table: "ProjectDeliverableHistory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDeliverableHistory_LastModifiedDate",
                table: "ProjectDeliverableHistory",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDeliverableHistory_ProjectHistoryId",
                table: "ProjectDeliverableHistory",
                column: "ProjectHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistory_ClientId",
                table: "ProjectHistory",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistory_Entity",
                table: "ProjectHistory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistory_LastModifiedDate",
                table: "ProjectHistory",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistory_PricingTypeId",
                table: "ProjectHistory",
                column: "PricingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistory_ProjectId",
                table: "ProjectHistory",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMilestone_Entity",
                table: "ProjectMilestone",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMilestone_FrequencyId",
                table: "ProjectMilestone",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMilestone_LastModifiedDate",
                table: "ProjectMilestone",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMilestone_MilestoneStageId",
                table: "ProjectMilestone",
                column: "MilestoneStageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMilestone_ProjectId",
                table: "ProjectMilestone",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMilestoneHistory_Entity",
                table: "ProjectMilestoneHistory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMilestoneHistory_FrequencyId",
                table: "ProjectMilestoneHistory",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMilestoneHistory_LastModifiedDate",
                table: "ProjectMilestoneHistory",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMilestoneHistory_MilestoneStageId",
                table: "ProjectMilestoneHistory",
                column: "MilestoneStageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMilestoneHistory_ProjectHistoryId",
                table: "ProjectMilestoneHistory",
                column: "ProjectHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackage_Entity",
                table: "ProjectPackage",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackage_LastModifiedDate",
                table: "ProjectPackage",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackage_ProjectId",
                table: "ProjectPackage",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackageAdditionalDeliverable_DeliverableId",
                table: "ProjectPackageAdditionalDeliverable",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackageAdditionalDeliverable_Entity",
                table: "ProjectPackageAdditionalDeliverable",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackageAdditionalDeliverable_LastModifiedDate",
                table: "ProjectPackageAdditionalDeliverable",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackageAdditionalDeliverable_ProjectPackageId",
                table: "ProjectPackageAdditionalDeliverable",
                column: "ProjectPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackageAdditionalDeliverableHistory_DeliverableId",
                table: "ProjectPackageAdditionalDeliverableHistory",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackageAdditionalDeliverableHistory_Entity",
                table: "ProjectPackageAdditionalDeliverableHistory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackageAdditionalDeliverableHistory_LastModifiedDate",
                table: "ProjectPackageAdditionalDeliverableHistory",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackageAdditionalDeliverableHistory_ProjectPackageHistoryId",
                table: "ProjectPackageAdditionalDeliverableHistory",
                column: "ProjectPackageHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackageHistory_Entity",
                table: "ProjectPackageHistory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackageHistory_LastModifiedDate",
                table: "ProjectPackageHistory",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackageHistory_ProjectHistoryId",
                table: "ProjectPackageHistory",
                column: "ProjectHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationConfiguration");

            migrationBuilder.DropTable(
                name: "Approval");

            migrationBuilder.DropTable(
                name: "ApproverAssignment");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "ProjectDeliverable");

            migrationBuilder.DropTable(
                name: "ProjectDeliverableHistory");

            migrationBuilder.DropTable(
                name: "ProjectMilestone");

            migrationBuilder.DropTable(
                name: "ProjectMilestoneHistory");

            migrationBuilder.DropTable(
                name: "ProjectPackageAdditionalDeliverable");

            migrationBuilder.DropTable(
                name: "ProjectPackageAdditionalDeliverableHistory");

            migrationBuilder.DropTable(
                name: "ApprovalRecord");

            migrationBuilder.DropTable(
                name: "Frequency");

            migrationBuilder.DropTable(
                name: "MilestoneStage");

            migrationBuilder.DropTable(
                name: "ProjectPackage");

            migrationBuilder.DropTable(
                name: "Deliverable");

            migrationBuilder.DropTable(
                name: "ProjectPackageHistory");

            migrationBuilder.DropTable(
                name: "ApproverSetup");

            migrationBuilder.DropTable(
                name: "ProjectCategory");

            migrationBuilder.DropTable(
                name: "ProjectHistory");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "PricingType");
        }
    }
}
