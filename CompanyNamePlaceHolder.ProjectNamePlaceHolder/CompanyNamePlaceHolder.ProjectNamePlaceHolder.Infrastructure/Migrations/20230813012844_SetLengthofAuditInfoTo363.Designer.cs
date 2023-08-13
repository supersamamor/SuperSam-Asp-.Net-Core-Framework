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
    [Migration("20230813012844_SetLengthofAuditInfoTo363")]
    partial class SetLengthofAuditInfoTo363
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CompanyNamePlaceHolder.Common.Data.Audit", b =>
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

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.MainModulePlaceHolderState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

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

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("MainModulePlaceHolder");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportColumnDetailState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArithmeticOperator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("FieldName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Function")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

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

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportColumnHeaderId");

                    b.HasIndex("ReportTableId");

                    b.ToTable("ReportColumnDetail");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportColumnFilterState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ComparisonOperator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("FieldName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsString")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

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

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportFilterGroupingId");

                    b.HasIndex("ReportTableId");

                    b.ToTable("ReportColumnFilter");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportColumnHeaderState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AggregationOperator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Alias")
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

                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportColumnHeader");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportFilterGroupingState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int?>("GroupLevel")
                        .HasColumnType("int");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LogicalOperator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportFilterGrouping");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportQueryFilterState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

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
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportQueryFilter");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportRoleAssignmentState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportRoleAssignment");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DisplayOnDashboard")
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

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportTableJoinParameterState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

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
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

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

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.HasIndex("ReportTableId");

                    b.ToTable("ReportTableJoinParameter");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportTableState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("JoinType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

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

                    b.HasIndex("CreatedBy");

                    b.HasIndex("Entity");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("LastModifiedDate");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportTable");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportColumnDetailState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportColumnHeaderState", "ReportColumnHeader")
                        .WithMany("ReportColumnDetailList")
                        .HasForeignKey("ReportColumnHeaderId");

                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportTableState", "ReportTable")
                        .WithMany("ReportColumnDetailList")
                        .HasForeignKey("ReportTableId");

                    b.Navigation("ReportColumnHeader");

                    b.Navigation("ReportTable");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportColumnFilterState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportFilterGroupingState", "ReportFilterGrouping")
                        .WithMany("ReportColumnFilterList")
                        .HasForeignKey("ReportFilterGroupingId");

                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportTableState", "ReportTable")
                        .WithMany("ReportColumnFilterList")
                        .HasForeignKey("ReportTableId");

                    b.Navigation("ReportFilterGrouping");

                    b.Navigation("ReportTable");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportColumnHeaderState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportState", "Report")
                        .WithMany("ReportColumnHeaderList")
                        .HasForeignKey("ReportId");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportFilterGroupingState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportState", "Report")
                        .WithMany("ReportFilterGroupingList")
                        .HasForeignKey("ReportId");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportQueryFilterState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportState", "Report")
                        .WithMany("ReportQueryFilterList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportRoleAssignmentState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportState", "Report")
                        .WithMany("ReportRoleAssignmentList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportTableJoinParameterState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportState", "Report")
                        .WithMany("ReportTableJoinParameterList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportTableState", "ReportTable")
                        .WithMany("ReportTableJoinParameterList")
                        .HasForeignKey("ReportTableId");

                    b.Navigation("Report");

                    b.Navigation("ReportTable");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportTableState", b =>
                {
                    b.HasOne("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportState", "Report")
                        .WithMany("ReportTableList")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportColumnHeaderState", b =>
                {
                    b.Navigation("ReportColumnDetailList");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportFilterGroupingState", b =>
                {
                    b.Navigation("ReportColumnFilterList");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportState", b =>
                {
                    b.Navigation("ReportColumnHeaderList");

                    b.Navigation("ReportFilterGroupingList");

                    b.Navigation("ReportQueryFilterList");

                    b.Navigation("ReportRoleAssignmentList");

                    b.Navigation("ReportTableJoinParameterList");

                    b.Navigation("ReportTableList");
                });

            modelBuilder.Entity("CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder.ReportTableState", b =>
                {
                    b.Navigation("ReportColumnDetailList");

                    b.Navigation("ReportColumnFilterList");

                    b.Navigation("ReportTableJoinParameterList");
                });
#pragma warning restore 612, 618
        }
    }
}
