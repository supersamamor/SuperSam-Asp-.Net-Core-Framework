﻿// <auto-generated />
using System;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231026080045_InitialDatabase")]
    partial class InitialDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CompanyNamePlaceHolder.Common.Data.Audit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    b.HasIndex("Id");

                    b.HasIndex("PrimaryKey");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ApprovalRecordState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("ApproverSetupId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DataId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ApproverSetupId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("DataId");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("Status");

                    b.ToTable("ApprovalRecord");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ApprovalState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("ApprovalRecordId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("ApprovalRemarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApproverUserId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

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
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

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

                    b.HasIndex("CreatedBy");

                    b.HasIndex("EmailSendingStatus");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("Status");

                    b.ToTable("Approval");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ApproverAssignmentState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("ApproverRoleId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("ApproverSetupId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("ApproverType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApproverUserId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ApproverSetupId", "ApproverUserId", "ApproverRoleId")
                        .IsUnique()
                        .HasFilter("[ApproverUserId] IS NOT NULL AND [ApproverRoleId] IS NOT NULL");

                    b.ToTable("ApproverAssignment");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ApproverSetupState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("ApprovalSetupType")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApprovalType")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

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
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

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

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("WorkflowName", "ApprovalSetupType", "TableName", "Entity")
                        .IsUnique()
                        .HasFilter("[WorkflowName] IS NOT NULL AND [TableName] IS NOT NULL AND [Entity] IS NOT NULL");

                    b.ToTable("ApproverSetup");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.AssignmentState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("AlternateAsignee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssignmentCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PrimaryAsignee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TaskListCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentCode")
                        .IsUnique();

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("TaskListCode");

                    b.ToTable("Assignment");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.DeliveryState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("ActualDeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ActualDeliveryRemarks")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ApprovedTag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApproverRemarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssignmentCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeliveryAttachment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeliveryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndorsedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndorsedTag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndorserRemarks")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("Delivery");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ReportQueryFilterState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

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
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("FieldDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportQueryFilter");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ReportRoleAssignmentState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportRoleAssignment");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ReportState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DisplayOnDashboard")
                        .HasColumnType("bit");

                    b.Property<bool>("DisplayOnReportModule")
                        .HasColumnType("bit");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<bool>("IsDistinct")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

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

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.TaskListState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("AlternateApprover")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlternateEndorser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

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
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TaskType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("TaskListCode")
                        .IsUnique();

                    b.ToTable("TaskList");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.UploadProcessorState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Module")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UploadType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("UploadProcessor");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.UploadStagingState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UploadProcessorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("UploadProcessorId");

                    b.ToTable("UploadStaging");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ApprovalRecordState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ApproverSetupState", "ApproverSetup")
                        .WithMany()
                        .HasForeignKey("ApproverSetupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApproverSetup");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ApprovalState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ApprovalRecordState", "ApprovalRecord")
                        .WithMany("ApprovalList")
                        .HasForeignKey("ApprovalRecordId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApprovalRecord");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ApproverAssignmentState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ApproverSetupState", "ApproverSetup")
                        .WithMany("ApproverAssignmentList")
                        .HasForeignKey("ApproverSetupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApproverSetup");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.AssignmentState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.TaskListState", "TaskList")
                        .WithMany("AssignmentList")
                        .HasForeignKey("TaskListCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaskList");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ReportQueryFilterState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ReportState", "Report")
                        .WithMany("ReportQueryFilterList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ReportRoleAssignmentState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ReportState", "Report")
                        .WithMany("ReportRoleAssignmentList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.UploadStagingState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.UploadProcessorState", "UploadProcessor")
                        .WithMany("UploadStagingList")
                        .HasForeignKey("UploadProcessorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UploadProcessor");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ApprovalRecordState", b =>
                {
                    b.Navigation("ApprovalList");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ApproverSetupState", b =>
                {
                    b.Navigation("ApproverAssignmentList");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.ReportState", b =>
                {
                    b.Navigation("ReportQueryFilterList");

                    b.Navigation("ReportRoleAssignmentList");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.TaskListState", b =>
                {
                    b.Navigation("AssignmentList");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder.UploadProcessorState", b =>
                {
                    b.Navigation("UploadStagingList");
                });
#pragma warning restore 612, 618
        }
    }
}
