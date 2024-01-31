﻿// <auto-generated />
using System;
using CompanyPL.ProjectPL.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CompanyPL.ProjectPL.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240130060315_InitialDatabase")]
    partial class InitialDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CompanyPL.Common.Data.Audit", b =>
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

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ApprovalRecordState", b =>
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

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ApprovalState", b =>
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

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ApproverAssignmentState", b =>
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

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ApproverSetupState", b =>
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

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ContactInformationState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("ContactDetails")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("ContactInformation");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.EmployeeState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmployeeCode")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("EmployeeCode")
                        .IsUnique();

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.HealthDeclarationState", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<bool?>("IsVaccinated")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Vaccine")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("Entity");

                    b.HasIndex("Id");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("HealthDeclaration");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ReportQueryFilterState", b =>
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

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ReportRoleAssignmentState", b =>
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

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ReportState", b =>
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

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.UploadProcessorState", b =>
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

                    b.Property<string>("ExceptionFilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.UploadStagingState", b =>
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

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ApprovalRecordState", b =>
                {
                    b.HasOne("CompanyPL.ProjectPL.Core.ProjectPL.ApproverSetupState", "ApproverSetup")
                        .WithMany()
                        .HasForeignKey("ApproverSetupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApproverSetup");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ApprovalState", b =>
                {
                    b.HasOne("CompanyPL.ProjectPL.Core.ProjectPL.ApprovalRecordState", "ApprovalRecord")
                        .WithMany("ApprovalList")
                        .HasForeignKey("ApprovalRecordId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApprovalRecord");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ApproverAssignmentState", b =>
                {
                    b.HasOne("CompanyPL.ProjectPL.Core.ProjectPL.ApproverSetupState", "ApproverSetup")
                        .WithMany("ApproverAssignmentList")
                        .HasForeignKey("ApproverSetupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApproverSetup");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ContactInformationState", b =>
                {
                    b.HasOne("CompanyPL.ProjectPL.Core.ProjectPL.EmployeeState", "Employee")
                        .WithMany("ContactInformationList")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.HealthDeclarationState", b =>
                {
                    b.HasOne("CompanyPL.ProjectPL.Core.ProjectPL.EmployeeState", "Employee")
                        .WithMany("HealthDeclarationList")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ReportQueryFilterState", b =>
                {
                    b.HasOne("CompanyPL.ProjectPL.Core.ProjectPL.ReportState", "Report")
                        .WithMany("ReportQueryFilterList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ReportRoleAssignmentState", b =>
                {
                    b.HasOne("CompanyPL.ProjectPL.Core.ProjectPL.ReportState", "Report")
                        .WithMany("ReportRoleAssignmentList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.UploadStagingState", b =>
                {
                    b.HasOne("CompanyPL.ProjectPL.Core.ProjectPL.UploadProcessorState", "UploadProcessor")
                        .WithMany("UploadStagingList")
                        .HasForeignKey("UploadProcessorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UploadProcessor");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ApprovalRecordState", b =>
                {
                    b.Navigation("ApprovalList");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ApproverSetupState", b =>
                {
                    b.Navigation("ApproverAssignmentList");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.EmployeeState", b =>
                {
                    b.Navigation("ContactInformationList");

                    b.Navigation("HealthDeclarationList");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.ReportState", b =>
                {
                    b.Navigation("ReportQueryFilterList");

                    b.Navigation("ReportRoleAssignmentList");
                });

            modelBuilder.Entity("CompanyPL.ProjectPL.Core.ProjectPL.UploadProcessorState", b =>
                {
                    b.Navigation("UploadStagingList");
                });
#pragma warning restore 612, 618
        }
    }
}
