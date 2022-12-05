﻿// <auto-generated />
using System;
using CTI.FAS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
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

            modelBuilder.Entity("CTI.FAS.Core.FAS.ApprovalRecordState", b =>
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

            modelBuilder.Entity("CTI.FAS.Core.FAS.ApprovalState", b =>
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

            modelBuilder.Entity("CTI.FAS.Core.FAS.ApproverAssignmentState", b =>
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

            modelBuilder.Entity("CTI.FAS.Core.FAS.ApproverSetupState", b =>
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

            modelBuilder.Entity("CTI.FAS.Core.FAS.BatchState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Batch")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("Batch");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.CompanyState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("AccountNumber")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("AccountType")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("BankCode")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("BankName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DatabaseConnectionSetupId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmailTelephoneNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EntityAddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("EntityAddressSecondLine")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("EntityDescription")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("EntityShortName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ImageLogo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("SubmitDeadline")
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("SubmitPlace")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("TINNo")
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.HasKey("Id");

                    b.HasIndex("DatabaseConnectionSetupId");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.CreditorState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreditorAccount")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DatabaseConnectionSetupId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PayeeAccountAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PayeeAccountCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PayeeAccountLongDescription")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("PayeeAccountName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PayeeAccountTIN")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("DatabaseConnectionSetupId");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("PayeeAccountName");

                    b.ToTable("Creditor");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.DatabaseConnectionSetupState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DatabaseAndServerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InhouseDatabaseAndServerName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SystemConnectionString")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("DatabaseConnectionSetup");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.EnrolledPayeeEmailState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("EnrolledPayeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EnrolledPayeeId");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("EnrolledPayeeEmail");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.EnrolledPayeeState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreditorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PayeeAccountNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PayeeAccountType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Status")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("CreditorId");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("CompanyId", "CreditorId")
                        .IsUnique();

                    b.ToTable("EnrolledPayee");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.PaymentTransactionState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BatchId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CheckNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DocumentAmount")
                        .HasColumnType("decimal(18,6)");

                    b.Property<DateTime>("DocumentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("EmailSentCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EmailSentDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Emailed")
                        .HasColumnType("bit");

                    b.Property<string>("EnrolledPayeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GroupCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("IfcaBatchNumber")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("IfcaLineNumber")
                        .HasColumnType("decimal(18,6)");

                    b.Property<bool>("IsForSending")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("PdfReport")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("TextFileName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("TransmissionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BatchId");

                    b.HasIndex("EnrolledPayeeId");

                    b.HasIndex("Entity");

                    b.HasIndex("IfcaBatchNumber");

                    b.HasIndex("IfcaLineNumber");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("PaymentTransaction");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.ProjectState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("GLA")
                        .HasColumnType("decimal(18,6)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<decimal>("LandArea")
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ProjectAddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.TenantState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tenant");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.UserEntityState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

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

                    b.Property<string>("PplusUserId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("UserEntity");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.ApprovalRecordState", b =>
                {
                    b.HasOne("CTI.FAS.Core.FAS.ApproverSetupState", "ApproverSetup")
                        .WithMany()
                        .HasForeignKey("ApproverSetupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApproverSetup");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.ApprovalState", b =>
                {
                    b.HasOne("CTI.FAS.Core.FAS.ApprovalRecordState", "ApprovalRecord")
                        .WithMany("ApprovalList")
                        .HasForeignKey("ApprovalRecordId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApprovalRecord");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.ApproverAssignmentState", b =>
                {
                    b.HasOne("CTI.FAS.Core.FAS.ApproverSetupState", "ApproverSetup")
                        .WithMany("ApproverAssignmentList")
                        .HasForeignKey("ApproverSetupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApproverSetup");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.CompanyState", b =>
                {
                    b.HasOne("CTI.FAS.Core.FAS.DatabaseConnectionSetupState", "DatabaseConnectionSetup")
                        .WithMany("CompanyList")
                        .HasForeignKey("DatabaseConnectionSetupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DatabaseConnectionSetup");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.CreditorState", b =>
                {
                    b.HasOne("CTI.FAS.Core.FAS.DatabaseConnectionSetupState", "DatabaseConnectionSetup")
                        .WithMany("CreditorList")
                        .HasForeignKey("DatabaseConnectionSetupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DatabaseConnectionSetup");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.EnrolledPayeeEmailState", b =>
                {
                    b.HasOne("CTI.FAS.Core.FAS.EnrolledPayeeState", "EnrolledPayee")
                        .WithMany("EnrolledPayeeEmailList")
                        .HasForeignKey("EnrolledPayeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EnrolledPayee");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.EnrolledPayeeState", b =>
                {
                    b.HasOne("CTI.FAS.Core.FAS.CompanyState", "Company")
                        .WithMany("EnrolledPayeeList")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CTI.FAS.Core.FAS.CreditorState", "Creditor")
                        .WithMany("EnrolledPayeeList")
                        .HasForeignKey("CreditorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Creditor");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.PaymentTransactionState", b =>
                {
                    b.HasOne("CTI.FAS.Core.FAS.BatchState", "Batch")
                        .WithMany("PaymentTransactionList")
                        .HasForeignKey("BatchId");

                    b.HasOne("CTI.FAS.Core.FAS.EnrolledPayeeState", "EnrolledPayee")
                        .WithMany("PaymentTransactionList")
                        .HasForeignKey("EnrolledPayeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Batch");

                    b.Navigation("EnrolledPayee");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.ProjectState", b =>
                {
                    b.HasOne("CTI.FAS.Core.FAS.CompanyState", "Company")
                        .WithMany("ProjectList")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.TenantState", b =>
                {
                    b.HasOne("CTI.FAS.Core.FAS.ProjectState", "Project")
                        .WithMany("TenantList")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.UserEntityState", b =>
                {
                    b.HasOne("CTI.FAS.Core.FAS.CompanyState", "Company")
                        .WithMany("UserEntityList")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.ApprovalRecordState", b =>
                {
                    b.Navigation("ApprovalList");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.ApproverSetupState", b =>
                {
                    b.Navigation("ApproverAssignmentList");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.BatchState", b =>
                {
                    b.Navigation("PaymentTransactionList");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.CompanyState", b =>
                {
                    b.Navigation("EnrolledPayeeList");

                    b.Navigation("ProjectList");

                    b.Navigation("UserEntityList");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.CreditorState", b =>
                {
                    b.Navigation("EnrolledPayeeList");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.DatabaseConnectionSetupState", b =>
                {
                    b.Navigation("CompanyList");

                    b.Navigation("CreditorList");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.EnrolledPayeeState", b =>
                {
                    b.Navigation("EnrolledPayeeEmailList");

                    b.Navigation("PaymentTransactionList");
                });

            modelBuilder.Entity("CTI.FAS.Core.FAS.ProjectState", b =>
                {
                    b.Navigation("TenantList");
                });
#pragma warning restore 612, 618
        }
    }
}
