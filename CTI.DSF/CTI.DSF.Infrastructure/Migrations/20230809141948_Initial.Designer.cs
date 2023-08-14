﻿// <auto-generated />
using System;
using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CTI.DSF.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230809141948_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CTI.Common.Data.Audit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AffectedColumns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryKey")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("TableName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TraceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryKey");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ApprovalRecordState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApproverSetupId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DataId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ApproverSetupId");

                    b.HasIndex("DataId");

                    b.HasIndex("Status");

                    b.ToTable("ApprovalRecord");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ApprovalState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApprovalRecordId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApprovalRemarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApproverUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EmailSendingDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailSendingRemarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailSendingStatus")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("StatusUpdateDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApprovalRecordId");

                    b.HasIndex("ApproverUserId");

                    b.HasIndex("EmailSendingStatus");

                    b.HasIndex("Status");

                    b.ToTable("Approval");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ApproverAssignmentState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApproverRoleId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApproverSetupId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApproverType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApproverUserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApproverSetupId", "ApproverUserId", "ApproverRoleId")
                        .IsUnique()
                        .HasFilter("[ApproverUserId] IS NOT NULL AND [ApproverRoleId] IS NOT NULL");

                    b.ToTable("ApproverAssignment");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ApproverSetupState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApprovalSetupType")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApprovalType")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailSubject")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TableName")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WorkflowDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkflowName")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("WorkflowName", "ApprovalSetupType", "TableName", "Entity")
                        .IsUnique()
                        .HasFilter("[WorkflowName] IS NOT NULL AND [TableName] IS NOT NULL AND [Entity] IS NOT NULL");

                    b.ToTable("ApproverSetup");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.AssignmentState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AlternateAsignee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssignmentCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PrimaryAsignee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TaskListCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskListId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentCode")
                        .IsUnique()
                        .HasFilter("[AssignmentCode] IS NOT NULL");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("TaskListId");

                    b.ToTable("Assignment");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.DeliveryState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssignmentCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssignmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeliveryAttachment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeliveryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("DeliveryCode")
                        .IsUnique();

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("Delivery");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportColumnDetailState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArithmeticOperator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Function")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportColumnHeaderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReportColumnId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportTableId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Sequence")
                        .HasColumnType("int");

                    b.Property<string>("TableId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReportColumnHeaderId");

                    b.HasIndex("ReportTableId");

                    b.ToTable("ReportColumnDetail");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportColumnFilterState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ComparisonOperator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsString")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LogicalOperator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportFilterGroupingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReportTableId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TableId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReportFilterGroupingId");

                    b.HasIndex("ReportTableId");

                    b.ToTable("ReportColumnFilter");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportColumnHeaderState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AggregationOperator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportColumnHeader");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportFilterGroupingState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupLevel")
                        .HasColumnType("int");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LogicalOperator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportFilterGrouping");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportQueryFilterState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomDropdownValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DropdownFilter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DropdownTableKeyAndValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportQueryFilter");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportRoleAssignmentState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportRoleAssignment");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DisplayOnDashboard")
                        .HasColumnType("bit");

                    b.Property<bool>("DisplayOnReportModule")
                        .HasColumnType("bit");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDistinct")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("QueryString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QueryType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportOrChartType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportTableJoinParameterState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JoinFromFieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JoinFromTableId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LogicalOperator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReportTableId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.Property<string>("TableId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.HasIndex("ReportTableId");

                    b.ToTable("ReportTableJoinParameter");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportTableState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JoinType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportTable");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.TaskListState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AlternateApprover")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlternateEndorser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PrimaryApprover")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryEndorser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TargetDueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TaskDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("TaskDueDay")
                        .HasColumnType("int");

                    b.Property<string>("TaskFrequency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskListCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TaskType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("TaskListCode")
                        .IsUnique()
                        .HasFilter("[TaskListCode] IS NOT NULL");

                    b.ToTable("TaskList");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ApprovalRecordState", b =>
                {
                    b.HasOne("CTI.DSF.Core.DSF.ApproverSetupState", "ApproverSetup")
                        .WithMany()
                        .HasForeignKey("ApproverSetupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApproverSetup");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ApprovalState", b =>
                {
                    b.HasOne("CTI.DSF.Core.DSF.ApprovalRecordState", "ApprovalRecord")
                        .WithMany("ApprovalList")
                        .HasForeignKey("ApprovalRecordId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApprovalRecord");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ApproverAssignmentState", b =>
                {
                    b.HasOne("CTI.DSF.Core.DSF.ApproverSetupState", "ApproverSetup")
                        .WithMany("ApproverAssignmentList")
                        .HasForeignKey("ApproverSetupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApproverSetup");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.AssignmentState", b =>
                {
                    b.HasOne("CTI.DSF.Core.DSF.TaskListState", "TaskList")
                        .WithMany("AssignmentList")
                        .HasForeignKey("TaskListId");

                    b.Navigation("TaskList");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.DeliveryState", b =>
                {
                    b.HasOne("CTI.DSF.Core.DSF.AssignmentState", "Assignment")
                        .WithMany("DeliveryList")
                        .HasForeignKey("AssignmentId");

                    b.Navigation("Assignment");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportColumnDetailState", b =>
                {
                    b.HasOne("CTI.DSF.Core.DSF.ReportColumnHeaderState", "ReportColumnHeader")
                        .WithMany("ReportColumnDetailList")
                        .HasForeignKey("ReportColumnHeaderId");

                    b.HasOne("CTI.DSF.Core.DSF.ReportTableState", "ReportTable")
                        .WithMany("ReportColumnDetailList")
                        .HasForeignKey("ReportTableId");

                    b.Navigation("ReportColumnHeader");

                    b.Navigation("ReportTable");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportColumnFilterState", b =>
                {
                    b.HasOne("CTI.DSF.Core.DSF.ReportFilterGroupingState", "ReportFilterGrouping")
                        .WithMany("ReportColumnFilterList")
                        .HasForeignKey("ReportFilterGroupingId");

                    b.HasOne("CTI.DSF.Core.DSF.ReportTableState", "ReportTable")
                        .WithMany("ReportColumnFilterList")
                        .HasForeignKey("ReportTableId");

                    b.Navigation("ReportFilterGrouping");

                    b.Navigation("ReportTable");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportColumnHeaderState", b =>
                {
                    b.HasOne("CTI.DSF.Core.DSF.ReportState", "Report")
                        .WithMany("ReportColumnHeaderList")
                        .HasForeignKey("ReportId");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportFilterGroupingState", b =>
                {
                    b.HasOne("CTI.DSF.Core.DSF.ReportState", "Report")
                        .WithMany("ReportFilterGroupingList")
                        .HasForeignKey("ReportId");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportQueryFilterState", b =>
                {
                    b.HasOne("CTI.DSF.Core.DSF.ReportState", "Report")
                        .WithMany("ReportQueryFilterList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportRoleAssignmentState", b =>
                {
                    b.HasOne("CTI.DSF.Core.DSF.ReportState", "Report")
                        .WithMany("ReportRoleAssignmentList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportTableJoinParameterState", b =>
                {
                    b.HasOne("CTI.DSF.Core.DSF.ReportState", "Report")
                        .WithMany("ReportTableJoinParameterList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CTI.DSF.Core.DSF.ReportTableState", "ReportTable")
                        .WithMany("ReportTableJoinParameterList")
                        .HasForeignKey("ReportTableId");

                    b.Navigation("Report");

                    b.Navigation("ReportTable");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportTableState", b =>
                {
                    b.HasOne("CTI.DSF.Core.DSF.ReportState", "Report")
                        .WithMany("ReportTableList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ApprovalRecordState", b =>
                {
                    b.Navigation("ApprovalList");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ApproverSetupState", b =>
                {
                    b.Navigation("ApproverAssignmentList");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.AssignmentState", b =>
                {
                    b.Navigation("DeliveryList");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportColumnHeaderState", b =>
                {
                    b.Navigation("ReportColumnDetailList");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportFilterGroupingState", b =>
                {
                    b.Navigation("ReportColumnFilterList");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportState", b =>
                {
                    b.Navigation("ReportColumnHeaderList");

                    b.Navigation("ReportFilterGroupingList");

                    b.Navigation("ReportQueryFilterList");

                    b.Navigation("ReportRoleAssignmentList");

                    b.Navigation("ReportTableJoinParameterList");

                    b.Navigation("ReportTableList");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.ReportTableState", b =>
                {
                    b.Navigation("ReportColumnDetailList");

                    b.Navigation("ReportColumnFilterList");

                    b.Navigation("ReportTableJoinParameterList");
                });

            modelBuilder.Entity("CTI.DSF.Core.DSF.TaskListState", b =>
                {
                    b.Navigation("AssignmentList");
                });
#pragma warning restore 612, 618
        }
    }
}
