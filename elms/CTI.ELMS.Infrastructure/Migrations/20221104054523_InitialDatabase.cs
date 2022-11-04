using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.ELMS.Infrastructure.Migrations
{
    public partial class InitialDatabase : Migration
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
                name: "BusinessNature",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BusinessNatureName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BusinessNatureCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessNature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientFeedback",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientFeedbackName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientFeedback", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadSource",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeadSourceName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadSource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadTask",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeadTaskName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadTask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadTouchPoint",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeadTouchPointName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadTouchPoint", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NextStep",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NextStepTaskName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextStep", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OperationTypeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PPlusConnectionSetup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PPlusVersionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TablePrefix = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ConnectionString = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ExhibitThemeCodes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PPlusConnectionSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportTableYTDExpirySummary",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntityID = table.Column<int>(type: "int", nullable: true),
                    EntityShortName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LandArea = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TotalGLA = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ColumnName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExpiryLotArea = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Renewed = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    NewLeases = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    WithProspectNego = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    OrderBy = table.Column<int>(type: "int", nullable: true),
                    VerticalOrderBy = table.Column<int>(type: "int", nullable: true),
                    ReportYear = table.Column<int>(type: "int", nullable: true),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTableYTDExpirySummary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salutation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SalutationDescription = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salutation", x => x.Id);
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
                name: "BusinessNatureSubItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BusinessNatureSubItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessNatureID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    BusinessNatureSubItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessNatureSubItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessNatureSubItem_BusinessNature_BusinessNatureID",
                        column: x => x.BusinessNatureID,
                        principalTable: "BusinessNature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeadTaskClientFeedBack",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeadTaskId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClientFeedbackId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ActivityStatus = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadTaskClientFeedBack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadTaskClientFeedBack_ClientFeedback_ClientFeedbackId",
                        column: x => x.ClientFeedbackId,
                        principalTable: "ClientFeedback",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeadTaskClientFeedBack_LeadTask_LeadTaskId",
                        column: x => x.LeadTaskId,
                        principalTable: "LeadTask",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LeadTaskNextStep",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeadTaskId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClientFeedbackId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NextStepId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PCTDay = table.Column<int>(type: "int", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadTaskNextStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadTaskNextStep_ClientFeedback_ClientFeedbackId",
                        column: x => x.ClientFeedbackId,
                        principalTable: "ClientFeedback",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeadTaskNextStep_LeadTask_LeadTaskId",
                        column: x => x.LeadTaskId,
                        principalTable: "LeadTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeadTaskNextStep_NextStep_NextStepId",
                        column: x => x.NextStepId,
                        principalTable: "NextStep",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EntityGroup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PPlusConnectionSetupID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PPLUSEntityCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    EntityShortName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TINNo = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    EntityDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EntityAddress2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityGroup_PPlusConnectionSetup_PPlusConnectionSetupID",
                        column: x => x.PPlusConnectionSetupID,
                        principalTable: "PPlusConnectionSetup",
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
                name: "BusinessNatureCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BusinessNatureCategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessNatureCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessNatureSubItemID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessNatureCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessNatureCategory_BusinessNatureSubItem_BusinessNatureSubItemID",
                        column: x => x.BusinessNatureSubItemID,
                        principalTable: "BusinessNatureSubItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IFCATransactionType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TransGroup = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EntityID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Mode = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IFCATransactionType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IFCATransactionType_EntityGroup_EntityID",
                        column: x => x.EntityID,
                        principalTable: "EntityGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DatabaseSource = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EntityGroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IFCAProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PayableTo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProjectNameANSection = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LandArea = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    GLA = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    SignatoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SignatoryPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ANSignatoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ANSignatoryPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContractSignatory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContractSignatoryPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContractSignatoryProofOfIdentity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContractSignatoryWitness = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContractSignatoryWitnessPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OutsideFC = table.Column<bool>(type: "bit", nullable: false),
                    ProjectGreetingsSection = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProjectShortName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SignatureUpper = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SignatureLower = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HasAssociationDues = table.Column<bool>(type: "bit", nullable: false),
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
                        name: "FK_Project_EntityGroup_EntityGroupId",
                        column: x => x.EntityGroupId,
                        principalTable: "EntityGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lead",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Company = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeadSourceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeadTouchpointId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OperationTypeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BusinessNatureID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BusinessNatureSubItemID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BusinessNatureCategoryID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TINNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsFranchise = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lead_BusinessNature_BusinessNatureID",
                        column: x => x.BusinessNatureID,
                        principalTable: "BusinessNature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lead_BusinessNatureCategory_BusinessNatureCategoryID",
                        column: x => x.BusinessNatureCategoryID,
                        principalTable: "BusinessNatureCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lead_BusinessNatureSubItem_BusinessNatureSubItemID",
                        column: x => x.BusinessNatureSubItemID,
                        principalTable: "BusinessNatureSubItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lead_LeadSource_LeadSourceId",
                        column: x => x.LeadSourceId,
                        principalTable: "LeadSource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lead_LeadTouchPoint_LeadTouchpointId",
                        column: x => x.LeadTouchpointId,
                        principalTable: "LeadTouchPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lead_OperationType_OperationTypeID",
                        column: x => x.OperationTypeID,
                        principalTable: "OperationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IFCAARAllocation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TenantContractNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DocumentNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TransactionAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IFCAARAllocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IFCAARAllocation_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IFCAARLedger",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantContractNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DocumentNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Mode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LedgerDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TransactionWithHoldingTaxAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TransactionAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    LotNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LineNo = table.Column<int>(type: "int", nullable: true),
                    TaxScheme = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TransactionTaxBaseAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TransactionTaxAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    SequenceNo = table.Column<int>(type: "int", nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TransactionClass = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GLAccount = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProjectID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TradeName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TransactionDesc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IFCAARLedger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IFCAARLedger_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProjectID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LotBudget = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    LotArea = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    AvailabilityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CommencementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrentTenantContractNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unit_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProjectAssignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjectAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProjectAssignment_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeadID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProjectID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LeadTaskId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ActivityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientFeedbackId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NextStepId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActivityRemarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PCTDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activity_ClientFeedback_ClientFeedbackId",
                        column: x => x.ClientFeedbackId,
                        principalTable: "ClientFeedback",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activity_Lead_LeadID",
                        column: x => x.LeadID,
                        principalTable: "Lead",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activity_LeadTask_LeadTaskId",
                        column: x => x.LeadTaskId,
                        principalTable: "LeadTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activity_NextStep_NextStepId",
                        column: x => x.NextStepId,
                        principalTable: "NextStep",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activity_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeadID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ContactType = table.Column<int>(type: "int", nullable: true),
                    ContactDetails = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Lead_LeadID",
                        column: x => x.LeadID,
                        principalTable: "Lead",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContactPerson",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeadId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SalutationID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    IsSOARecipient = table.Column<bool>(type: "bit", nullable: false),
                    IsANSignatory = table.Column<bool>(type: "bit", nullable: false),
                    IsCOLSignatory = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPerson_Lead_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Lead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactPerson_Salutation_SalutationID",
                        column: x => x.SalutationID,
                        principalTable: "Salutation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Offering",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OfferingHistoryID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CommencementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TerminationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: true),
                    SecMonths = table.Column<int>(type: "int", nullable: true),
                    ConstructionMonths = table.Column<int>(type: "int", nullable: true),
                    OtherChargesAircon = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Concession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OffersheetRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConstructionCAMC = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    CommencementCAMC = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    BoardUp = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    UnitsInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ANType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LeadID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TenantContractNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocStamp = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    AwardNoticeCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AwardNoticeCreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    SignedOfferSheetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TagSignedOfferSheetBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalUnitArea = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    IsPOS = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalSecurityDeposit = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    AnnualAdvertisingFee = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    CAMCConstructionTotalUnitArea = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ConstructionCAMCMonths = table.Column<int>(type: "int", nullable: true),
                    ConstructionCAMCRate = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    HasBoardUpFee = table.Column<bool>(type: "bit", nullable: false),
                    SecurityDepositPayableWithinMonths = table.Column<int>(type: "int", nullable: true),
                    TotalConstructionBond = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ConstructionPayableWithinMonths = table.Column<int>(type: "int", nullable: true),
                    TotalBasicFixedMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TotalMinimumMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TotalLotBudget = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TotalPercentageRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    AutoComputeTotalConstructionBond = table.Column<bool>(type: "bit", nullable: false),
                    AutoComputeTotalSecurityDeposit = table.Column<bool>(type: "bit", nullable: false),
                    OfferSheetPerProjectCounter = table.Column<int>(type: "int", nullable: true),
                    SignedAwardNoticeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TagSignedAwardNoticeBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumSalesQuota = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    UnitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FitOutPeriod = table.Column<int>(type: "int", nullable: true),
                    TurnOverDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AutoComputeAnnualAdvertisingFee = table.Column<bool>(type: "bit", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SignatoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SignatoryPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ANSignatoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ANSignatoryPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LeaseContractCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LeaseContractCreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    TagSignedLeaseContractBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignedLeaseContractDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TagForReviewLeaseContractBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForReviewLeaseContractDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TagForFinalPrintLeaseContractBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForFinalPrintLeaseContractDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LeaseContractStatus = table.Column<int>(type: "int", nullable: true),
                    ANTermTypeID = table.Column<int>(type: "int", nullable: true),
                    ContractTypeID = table.Column<int>(type: "int", nullable: true),
                    WitnessName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PermittedUse = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedCategory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDisabledModifiedCategory = table.Column<bool>(type: "bit", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offering", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offering_Lead_LeadID",
                        column: x => x.LeadID,
                        principalTable: "Lead",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offering_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitBudget",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: true),
                    ProjectID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UnitID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    January = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    February = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    March = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    April = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    May = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    June = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    July = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    August = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    September = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    October = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    November = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    December = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    LotArea = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    IsOriginalBudgeted = table.Column<bool>(type: "bit", nullable: false),
                    ParentUnitBudgetID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitBudget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitBudget_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitBudget_Unit_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Unit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitBudget_UnitBudget_ParentUnitBudgetID",
                        column: x => x.ParentUnitBudgetID,
                        principalTable: "UnitBudget",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActivityHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActivityID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LeadTaskId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ActivityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientFeedbackId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NextStepId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActivityRemarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PCTDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UnitsInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityHistory_Activity_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActivityHistory_ClientFeedback_ClientFeedbackId",
                        column: x => x.ClientFeedbackId,
                        principalTable: "ClientFeedback",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActivityHistory_LeadTask_LeadTaskId",
                        column: x => x.LeadTaskId,
                        principalTable: "LeadTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActivityHistory_NextStep_NextStepId",
                        column: x => x.NextStepId,
                        principalTable: "NextStep",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IFCATenantInformation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OfferingID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantContractNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsExhibit = table.Column<bool>(type: "bit", nullable: false),
                    ProjectID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TradeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TINNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaidSecurityDeposit = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Allowance = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TenantCategory = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsAnchor = table.Column<bool>(type: "bit", nullable: false),
                    TenantClassification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IFCATenantInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IFCATenantInformation_Offering_OfferingID",
                        column: x => x.OfferingID,
                        principalTable: "Offering",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IFCATenantInformation_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferingHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OfferingID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    CommencementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TerminationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: true),
                    SecMonths = table.Column<int>(type: "int", nullable: true),
                    ConstructionMonths = table.Column<int>(type: "int", nullable: true),
                    BoardUp = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    OtherChargesAircon = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ConstructionCAMC = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    CommencementCAMC = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Concession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OffersheetRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitsInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ANType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LeadID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProjectID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TotalUnitArea = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TotalSecurityDeposit = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    AnnualAdvertisingFee = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    CAMCConstructionTotalUnitArea = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ConstructionCAMCMonths = table.Column<int>(type: "int", nullable: true),
                    ConstructionCAMCRate = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    HasBoardUpFee = table.Column<bool>(type: "bit", nullable: false),
                    SecurityDepositPayableWithinMonths = table.Column<int>(type: "int", nullable: true),
                    TotalConstructionBond = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ConstructionPayableWithinMonths = table.Column<int>(type: "int", nullable: true),
                    IsPOS = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferingVersion = table.Column<int>(type: "int", nullable: true),
                    TotalBasicFixedMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TotalMinimumMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TotalLotBudget = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TotalPercentageRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    AutoComputeTotalConstructionBond = table.Column<bool>(type: "bit", nullable: false),
                    AutoComputeTotalSecurityDeposit = table.Column<bool>(type: "bit", nullable: false),
                    MinimumSalesQuota = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    UnitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FitOutPeriod = table.Column<int>(type: "int", nullable: true),
                    TurnOverDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutoComputeAnnualAdvertisingFee = table.Column<bool>(type: "bit", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferingHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferingHistory_Lead_LeadID",
                        column: x => x.LeadID,
                        principalTable: "Lead",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfferingHistory_Offering_OfferingID",
                        column: x => x.OfferingID,
                        principalTable: "Offering",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfferingHistory_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PreSelectedUnit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OfferingID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UnitID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LotBudget = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    LotArea = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreSelectedUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreSelectedUnit_Offering_OfferingID",
                        column: x => x.OfferingID,
                        principalTable: "Offering",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreSelectedUnit_Unit_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Unit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitOffered",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LotBudget = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    LotArea = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    OfferingID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UnitID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BasicFixedMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PercentageRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    MinimumMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    AnnualIncrement = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    AnnualIncrementInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFixedMonthlyRent = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOffered", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitOffered_Offering_OfferingID",
                        column: x => x.OfferingID,
                        principalTable: "Offering",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitOffered_Unit_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Unit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitActivity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActivityHistoryID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ActivityID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitActivity_Activity_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitActivity_ActivityHistory_ActivityHistoryID",
                        column: x => x.ActivityHistoryID,
                        principalTable: "ActivityHistory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitActivity_Unit_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IFCAUnitInformation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IFCATenantInformationID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RentalRate = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    BudgetedAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BasicFixedMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IFCAUnitInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IFCAUnitInformation_IFCATenantInformation_IFCATenantInformationID",
                        column: x => x.IFCATenantInformationID,
                        principalTable: "IFCATenantInformation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IFCAUnitInformation_Unit_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Unit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportTableCollectionDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IFCATenantInformationID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTerminated = table.Column<bool>(type: "bit", nullable: false),
                    CurrentMonth = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PrevMonth1 = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PrevMonth2 = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PrevMonth3 = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Prior = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TotalOverDue = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Rental = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    CusaAC = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Utilities = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Deposits = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Interests = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Penalty = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Others = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PaidSD = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    SDExposure = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PayableCurrentMonth = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PayablePrevMonth1 = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PayablePrevMonth2 = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PayablePrevMonth3 = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PayablePrior = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Column1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Column2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Column3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Column4 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTableCollectionDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportTableCollectionDetail_IFCATenantInformation_IFCATenantInformationID",
                        column: x => x.IFCATenantInformationID,
                        principalTable: "IFCATenantInformation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportTableCollectionDetail_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitGroup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitsInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotArea = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    BasicFixedMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    OfferingHistoryID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UnitOfferedHistoryID = table.Column<int>(type: "int", nullable: true),
                    IsFixedMonthlyRent = table.Column<bool>(type: "bit", nullable: false),
                    AreaTypeDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitGroup_OfferingHistory_OfferingHistoryID",
                        column: x => x.OfferingHistoryID,
                        principalTable: "OfferingHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitOfferedHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OfferingID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UnitID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LotBudget = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    LotArea = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    OfferingHistoryID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BasicFixedMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PercentageRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    MinimumMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    AnnualIncrement = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    AnnualIncrementInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFixedMonthlyRent = table.Column<bool>(type: "bit", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfferedHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitOfferedHistory_Offering_OfferingID",
                        column: x => x.OfferingID,
                        principalTable: "Offering",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitOfferedHistory_OfferingHistory_OfferingHistoryID",
                        column: x => x.OfferingHistoryID,
                        principalTable: "OfferingHistory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitOfferedHistory_Unit_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Unit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnnualIncrement",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitOfferedID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    BasicFixedMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PercentageRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    MinimumMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualIncrement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualIncrement_UnitOffered_UnitOfferedID",
                        column: x => x.UnitOfferedID,
                        principalTable: "UnitOffered",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnnualIncrementHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitOfferedHistoryID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    BasicFixedMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PercentageRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    MinimumMonthlyRent = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ContractGroupingCount = table.Column<int>(type: "int", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualIncrementHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualIncrementHistory_UnitOfferedHistory_UnitOfferedHistoryID",
                        column: x => x.UnitOfferedHistoryID,
                        principalTable: "UnitOfferedHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ClientFeedbackId",
                table: "Activity",
                column: "ClientFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_Entity",
                table: "Activity",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_LastModifiedDate",
                table: "Activity",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_LeadID",
                table: "Activity",
                column: "LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_LeadTaskId",
                table: "Activity",
                column: "LeadTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_NextStepId",
                table: "Activity",
                column: "NextStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ProjectID",
                table: "Activity",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityHistory_ActivityID",
                table: "ActivityHistory",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityHistory_ClientFeedbackId",
                table: "ActivityHistory",
                column: "ClientFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityHistory_Entity",
                table: "ActivityHistory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityHistory_LastModifiedDate",
                table: "ActivityHistory",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityHistory_LeadTaskId",
                table: "ActivityHistory",
                column: "LeadTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityHistory_NextStepId",
                table: "ActivityHistory",
                column: "NextStepId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualIncrement_Entity",
                table: "AnnualIncrement",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualIncrement_LastModifiedDate",
                table: "AnnualIncrement",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualIncrement_UnitOfferedID",
                table: "AnnualIncrement",
                column: "UnitOfferedID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualIncrementHistory_Entity",
                table: "AnnualIncrementHistory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualIncrementHistory_LastModifiedDate",
                table: "AnnualIncrementHistory",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualIncrementHistory_UnitOfferedHistoryID",
                table: "AnnualIncrementHistory",
                column: "UnitOfferedHistoryID");

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
                name: "IX_BusinessNature_BusinessNatureCode",
                table: "BusinessNature",
                column: "BusinessNatureCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNature_BusinessNatureName",
                table: "BusinessNature",
                column: "BusinessNatureName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNature_Entity",
                table: "BusinessNature",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNature_LastModifiedDate",
                table: "BusinessNature",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNatureCategory_BusinessNatureSubItemID",
                table: "BusinessNatureCategory",
                column: "BusinessNatureSubItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNatureCategory_Entity",
                table: "BusinessNatureCategory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNatureCategory_LastModifiedDate",
                table: "BusinessNatureCategory",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNatureSubItem_BusinessNatureID",
                table: "BusinessNatureSubItem",
                column: "BusinessNatureID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNatureSubItem_Entity",
                table: "BusinessNatureSubItem",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNatureSubItem_LastModifiedDate",
                table: "BusinessNatureSubItem",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ClientFeedback_ClientFeedbackName",
                table: "ClientFeedback",
                column: "ClientFeedbackName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientFeedback_Entity",
                table: "ClientFeedback",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ClientFeedback_LastModifiedDate",
                table: "ClientFeedback",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Entity",
                table: "Contact",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_LastModifiedDate",
                table: "Contact",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_LeadID",
                table: "Contact",
                column: "LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPerson_Entity",
                table: "ContactPerson",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPerson_LastModifiedDate",
                table: "ContactPerson",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPerson_LeadId",
                table: "ContactPerson",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPerson_SalutationID",
                table: "ContactPerson",
                column: "SalutationID");

            migrationBuilder.CreateIndex(
                name: "IX_EntityGroup_Entity",
                table: "EntityGroup",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_EntityGroup_LastModifiedDate",
                table: "EntityGroup",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_EntityGroup_PPlusConnectionSetupID",
                table: "EntityGroup",
                column: "PPlusConnectionSetupID");

            migrationBuilder.CreateIndex(
                name: "IX_IFCAARAllocation_Entity",
                table: "IFCAARAllocation",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_IFCAARAllocation_LastModifiedDate",
                table: "IFCAARAllocation",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_IFCAARAllocation_ProjectID",
                table: "IFCAARAllocation",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_IFCAARLedger_Entity",
                table: "IFCAARLedger",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_IFCAARLedger_LastModifiedDate",
                table: "IFCAARLedger",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_IFCAARLedger_ProjectID",
                table: "IFCAARLedger",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_IFCATenantInformation_Entity",
                table: "IFCATenantInformation",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_IFCATenantInformation_LastModifiedDate",
                table: "IFCATenantInformation",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_IFCATenantInformation_OfferingID",
                table: "IFCATenantInformation",
                column: "OfferingID");

            migrationBuilder.CreateIndex(
                name: "IX_IFCATenantInformation_ProjectID",
                table: "IFCATenantInformation",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_IFCATransactionType_Entity",
                table: "IFCATransactionType",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_IFCATransactionType_EntityID",
                table: "IFCATransactionType",
                column: "EntityID");

            migrationBuilder.CreateIndex(
                name: "IX_IFCATransactionType_LastModifiedDate",
                table: "IFCATransactionType",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_IFCAUnitInformation_Entity",
                table: "IFCAUnitInformation",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_IFCAUnitInformation_IFCATenantInformationID",
                table: "IFCAUnitInformation",
                column: "IFCATenantInformationID");

            migrationBuilder.CreateIndex(
                name: "IX_IFCAUnitInformation_LastModifiedDate",
                table: "IFCAUnitInformation",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_IFCAUnitInformation_UnitID",
                table: "IFCAUnitInformation",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_BusinessNatureCategoryID",
                table: "Lead",
                column: "BusinessNatureCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_BusinessNatureID",
                table: "Lead",
                column: "BusinessNatureID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_BusinessNatureSubItemID",
                table: "Lead",
                column: "BusinessNatureSubItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_Entity",
                table: "Lead",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_LastModifiedDate",
                table: "Lead",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_LeadSourceId",
                table: "Lead",
                column: "LeadSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_LeadTouchpointId",
                table: "Lead",
                column: "LeadTouchpointId");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_OperationTypeID",
                table: "Lead",
                column: "OperationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadSource_Entity",
                table: "LeadSource",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_LeadSource_LastModifiedDate",
                table: "LeadSource",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_LeadSource_LeadSourceName",
                table: "LeadSource",
                column: "LeadSourceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeadTask_Entity",
                table: "LeadTask",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_LeadTask_LastModifiedDate",
                table: "LeadTask",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_LeadTask_LeadTaskName",
                table: "LeadTask",
                column: "LeadTaskName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeadTaskClientFeedBack_ClientFeedbackId",
                table: "LeadTaskClientFeedBack",
                column: "ClientFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadTaskClientFeedBack_Entity",
                table: "LeadTaskClientFeedBack",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_LeadTaskClientFeedBack_LastModifiedDate",
                table: "LeadTaskClientFeedBack",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_LeadTaskClientFeedBack_LeadTaskId",
                table: "LeadTaskClientFeedBack",
                column: "LeadTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadTaskNextStep_ClientFeedbackId",
                table: "LeadTaskNextStep",
                column: "ClientFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadTaskNextStep_Entity",
                table: "LeadTaskNextStep",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_LeadTaskNextStep_LastModifiedDate",
                table: "LeadTaskNextStep",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_LeadTaskNextStep_LeadTaskId",
                table: "LeadTaskNextStep",
                column: "LeadTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadTaskNextStep_NextStepId",
                table: "LeadTaskNextStep",
                column: "NextStepId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadTouchPoint_Entity",
                table: "LeadTouchPoint",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_LeadTouchPoint_LastModifiedDate",
                table: "LeadTouchPoint",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_LeadTouchPoint_LeadTouchPointName",
                table: "LeadTouchPoint",
                column: "LeadTouchPointName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NextStep_Entity",
                table: "NextStep",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_NextStep_LastModifiedDate",
                table: "NextStep",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_NextStep_NextStepTaskName",
                table: "NextStep",
                column: "NextStepTaskName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offering_Entity",
                table: "Offering",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Offering_LastModifiedDate",
                table: "Offering",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Offering_LeadID",
                table: "Offering",
                column: "LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_Offering_ProjectID",
                table: "Offering",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferingHistory_Entity",
                table: "OfferingHistory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_OfferingHistory_LastModifiedDate",
                table: "OfferingHistory",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_OfferingHistory_LeadID",
                table: "OfferingHistory",
                column: "LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferingHistory_OfferingID",
                table: "OfferingHistory",
                column: "OfferingID");

            migrationBuilder.CreateIndex(
                name: "IX_OfferingHistory_ProjectID",
                table: "OfferingHistory",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationType_Entity",
                table: "OperationType",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_OperationType_LastModifiedDate",
                table: "OperationType",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_OperationType_OperationTypeName",
                table: "OperationType",
                column: "OperationTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PPlusConnectionSetup_Entity",
                table: "PPlusConnectionSetup",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_PPlusConnectionSetup_LastModifiedDate",
                table: "PPlusConnectionSetup",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_PPlusConnectionSetup_PPlusVersionName",
                table: "PPlusConnectionSetup",
                column: "PPlusVersionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreSelectedUnit_Entity",
                table: "PreSelectedUnit",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_PreSelectedUnit_LastModifiedDate",
                table: "PreSelectedUnit",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_PreSelectedUnit_OfferingID",
                table: "PreSelectedUnit",
                column: "OfferingID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSelectedUnit_UnitID",
                table: "PreSelectedUnit",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Entity",
                table: "Project",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Project_EntityGroupId",
                table: "Project",
                column: "EntityGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_LastModifiedDate",
                table: "Project",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableCollectionDetail_Entity",
                table: "ReportTableCollectionDetail",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableCollectionDetail_IFCATenantInformationID",
                table: "ReportTableCollectionDetail",
                column: "IFCATenantInformationID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableCollectionDetail_LastModifiedDate",
                table: "ReportTableCollectionDetail",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableCollectionDetail_ProjectID",
                table: "ReportTableCollectionDetail",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableYTDExpirySummary_Entity",
                table: "ReportTableYTDExpirySummary",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableYTDExpirySummary_LastModifiedDate",
                table: "ReportTableYTDExpirySummary",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Salutation_Entity",
                table: "Salutation",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Salutation_LastModifiedDate",
                table: "Salutation",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Salutation_SalutationDescription",
                table: "Salutation",
                column: "SalutationDescription",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_Entity",
                table: "Unit",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_LastModifiedDate",
                table: "Unit",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ProjectID",
                table: "Unit",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitActivity_ActivityHistoryID",
                table: "UnitActivity",
                column: "ActivityHistoryID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitActivity_ActivityID",
                table: "UnitActivity",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitActivity_Entity",
                table: "UnitActivity",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_UnitActivity_LastModifiedDate",
                table: "UnitActivity",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_UnitActivity_UnitID",
                table: "UnitActivity",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitBudget_Entity",
                table: "UnitBudget",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_UnitBudget_LastModifiedDate",
                table: "UnitBudget",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_UnitBudget_ParentUnitBudgetID",
                table: "UnitBudget",
                column: "ParentUnitBudgetID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitBudget_ProjectID",
                table: "UnitBudget",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitBudget_UnitID",
                table: "UnitBudget",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitGroup_Entity",
                table: "UnitGroup",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_UnitGroup_LastModifiedDate",
                table: "UnitGroup",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_UnitGroup_OfferingHistoryID",
                table: "UnitGroup",
                column: "OfferingHistoryID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOffered_Entity",
                table: "UnitOffered",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOffered_LastModifiedDate",
                table: "UnitOffered",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOffered_OfferingID",
                table: "UnitOffered",
                column: "OfferingID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOffered_UnitID",
                table: "UnitOffered",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfferedHistory_Entity",
                table: "UnitOfferedHistory",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfferedHistory_LastModifiedDate",
                table: "UnitOfferedHistory",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfferedHistory_OfferingHistoryID",
                table: "UnitOfferedHistory",
                column: "OfferingHistoryID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfferedHistory_OfferingID",
                table: "UnitOfferedHistory",
                column: "OfferingID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfferedHistory_UnitID",
                table: "UnitOfferedHistory",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectAssignment_Entity",
                table: "UserProjectAssignment",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectAssignment_LastModifiedDate",
                table: "UserProjectAssignment",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectAssignment_ProjectID",
                table: "UserProjectAssignment",
                column: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualIncrement");

            migrationBuilder.DropTable(
                name: "AnnualIncrementHistory");

            migrationBuilder.DropTable(
                name: "Approval");

            migrationBuilder.DropTable(
                name: "ApproverAssignment");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "ContactPerson");

            migrationBuilder.DropTable(
                name: "IFCAARAllocation");

            migrationBuilder.DropTable(
                name: "IFCAARLedger");

            migrationBuilder.DropTable(
                name: "IFCATransactionType");

            migrationBuilder.DropTable(
                name: "IFCAUnitInformation");

            migrationBuilder.DropTable(
                name: "LeadTaskClientFeedBack");

            migrationBuilder.DropTable(
                name: "LeadTaskNextStep");

            migrationBuilder.DropTable(
                name: "PreSelectedUnit");

            migrationBuilder.DropTable(
                name: "ReportTableCollectionDetail");

            migrationBuilder.DropTable(
                name: "ReportTableYTDExpirySummary");

            migrationBuilder.DropTable(
                name: "UnitActivity");

            migrationBuilder.DropTable(
                name: "UnitBudget");

            migrationBuilder.DropTable(
                name: "UnitGroup");

            migrationBuilder.DropTable(
                name: "UserProjectAssignment");

            migrationBuilder.DropTable(
                name: "UnitOffered");

            migrationBuilder.DropTable(
                name: "UnitOfferedHistory");

            migrationBuilder.DropTable(
                name: "ApprovalRecord");

            migrationBuilder.DropTable(
                name: "Salutation");

            migrationBuilder.DropTable(
                name: "IFCATenantInformation");

            migrationBuilder.DropTable(
                name: "ActivityHistory");

            migrationBuilder.DropTable(
                name: "OfferingHistory");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "ApproverSetup");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "Offering");

            migrationBuilder.DropTable(
                name: "ClientFeedback");

            migrationBuilder.DropTable(
                name: "LeadTask");

            migrationBuilder.DropTable(
                name: "NextStep");

            migrationBuilder.DropTable(
                name: "Lead");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "BusinessNatureCategory");

            migrationBuilder.DropTable(
                name: "LeadSource");

            migrationBuilder.DropTable(
                name: "LeadTouchPoint");

            migrationBuilder.DropTable(
                name: "OperationType");

            migrationBuilder.DropTable(
                name: "EntityGroup");

            migrationBuilder.DropTable(
                name: "BusinessNatureSubItem");

            migrationBuilder.DropTable(
                name: "PPlusConnectionSetup");

            migrationBuilder.DropTable(
                name: "BusinessNature");
        }
    }
}
