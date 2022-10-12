// <auto-generated />
using System;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CTI.SQLReportAutoSender.Infrastructure.Migrations
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

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ApprovalRecordState", b =>
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

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ApprovalState", b =>
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

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ApproverAssignmentState", b =>
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

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ApproverSetupState", b =>
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

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.CustomScheduleState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTimeSchedule")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.ToTable("CustomSchedule");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.MailRecipientState", b =>
                {
                    b.Property<string>("Id")
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

                    b.Property<string>("RecipientEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.ToTable("MailRecipient");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.MailSettingState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(2500)
                        .HasColumnType("nvarchar(2500)");

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

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.ToTable("MailSetting");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportDetailState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConnectionString")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("QueryString")
                        .IsRequired()
                        .HasMaxLength(8000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReportDetailNumber")
                        .HasColumnType("int");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportDetail");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportInboxState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTimeSent")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportInbox");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportScheduleSettingState", b =>
                {
                    b.Property<string>("Id")
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

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ScheduleFrequencyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ScheduleParameterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.HasIndex("ScheduleFrequencyId");

                    b.HasIndex("ScheduleParameterId");

                    b.ToTable("ReportScheduleSetting");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LatestFileGeneratedPath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("MultipleReportType")
                        .HasColumnType("int");

                    b.Property<string>("ScheduleFrequencyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ScheduleFrequencyId");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ScheduleFrequencyParameterSetupState", b =>
                {
                    b.Property<string>("Id")
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

                    b.Property<string>("ScheduleFrequencyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ScheduleParameterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ScheduleFrequencyId");

                    b.HasIndex("ScheduleParameterId");

                    b.ToTable("ScheduleFrequencyParameterSetup");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ScheduleFrequencyState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Description")
                        .IsUnique();

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("ScheduleFrequency");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ScheduleParameterState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Entity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Description")
                        .IsUnique();

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("ScheduleParameter");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ApprovalRecordState", b =>
                {
                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ApproverSetupState", "ApproverSetup")
                        .WithMany()
                        .HasForeignKey("ApproverSetupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApproverSetup");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ApprovalState", b =>
                {
                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ApprovalRecordState", "ApprovalRecord")
                        .WithMany("ApprovalList")
                        .HasForeignKey("ApprovalRecordId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApprovalRecord");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ApproverAssignmentState", b =>
                {
                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ApproverSetupState", "ApproverSetup")
                        .WithMany("ApproverAssignmentList")
                        .HasForeignKey("ApproverSetupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApproverSetup");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.CustomScheduleState", b =>
                {
                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportState", "Report")
                        .WithMany("CustomScheduleList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.MailRecipientState", b =>
                {
                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportState", "Report")
                        .WithMany("MailRecipientList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.MailSettingState", b =>
                {
                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportState", "Report")
                        .WithMany("MailSettingList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportDetailState", b =>
                {
                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportState", "Report")
                        .WithMany("ReportDetailList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportInboxState", b =>
                {
                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportState", "Report")
                        .WithMany("ReportInboxList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportScheduleSettingState", b =>
                {
                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportState", "Report")
                        .WithMany("ReportScheduleSettingList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ScheduleFrequencyState", "ScheduleFrequency")
                        .WithMany("ReportScheduleSettingList")
                        .HasForeignKey("ScheduleFrequencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ScheduleParameterState", "ScheduleParameter")
                        .WithMany("ReportScheduleSettingList")
                        .HasForeignKey("ScheduleParameterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");

                    b.Navigation("ScheduleFrequency");

                    b.Navigation("ScheduleParameter");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportState", b =>
                {
                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ScheduleFrequencyState", "ScheduleFrequency")
                        .WithMany("ReportList")
                        .HasForeignKey("ScheduleFrequencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ScheduleFrequency");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ScheduleFrequencyParameterSetupState", b =>
                {
                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ScheduleFrequencyState", "ScheduleFrequency")
                        .WithMany("ScheduleFrequencyParameterSetupList")
                        .HasForeignKey("ScheduleFrequencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ScheduleParameterState", "ScheduleParameter")
                        .WithMany("ScheduleFrequencyParameterSetupList")
                        .HasForeignKey("ScheduleParameterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ScheduleFrequency");

                    b.Navigation("ScheduleParameter");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ApprovalRecordState", b =>
                {
                    b.Navigation("ApprovalList");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ApproverSetupState", b =>
                {
                    b.Navigation("ApproverAssignmentList");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ReportState", b =>
                {
                    b.Navigation("CustomScheduleList");

                    b.Navigation("MailRecipientList");

                    b.Navigation("MailSettingList");

                    b.Navigation("ReportDetailList");

                    b.Navigation("ReportInboxList");

                    b.Navigation("ReportScheduleSettingList");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ScheduleFrequencyState", b =>
                {
                    b.Navigation("ReportList");

                    b.Navigation("ReportScheduleSettingList");

                    b.Navigation("ScheduleFrequencyParameterSetupList");
                });

            modelBuilder.Entity("CTI.SQLReportAutoSender.Core.SQLReportAutoSender.ScheduleParameterState", b =>
                {
                    b.Navigation("ReportScheduleSettingList");

                    b.Navigation("ScheduleFrequencyParameterSetupList");
                });
#pragma warning restore 612, 618
        }
    }
}
