using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations
{
    public partial class SetLengthofAuditInfoTo362 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportTableJoinParameter",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportTable",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportRoleAssignment",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportQueryFilter",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportFilterGrouping",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportColumnHeader",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportColumnFilter",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportColumnDetail",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "Report",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "MainModulePlaceHolder",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_CreatedBy",
                table: "ReportTableJoinParameter",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_Entity",
                table: "ReportTableJoinParameter",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_LastModifiedBy",
                table: "ReportTableJoinParameter",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_CreatedBy",
                table: "ReportTable",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_Entity",
                table: "ReportTable",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_LastModifiedBy",
                table: "ReportTable",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_CreatedBy",
                table: "ReportRoleAssignment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_Entity",
                table: "ReportRoleAssignment",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_LastModifiedBy",
                table: "ReportRoleAssignment",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_CreatedBy",
                table: "ReportQueryFilter",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_Entity",
                table: "ReportQueryFilter",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_LastModifiedBy",
                table: "ReportQueryFilter",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_CreatedBy",
                table: "ReportFilterGrouping",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_Entity",
                table: "ReportFilterGrouping",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_LastModifiedBy",
                table: "ReportFilterGrouping",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_CreatedBy",
                table: "ReportColumnHeader",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_Entity",
                table: "ReportColumnHeader",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_LastModifiedBy",
                table: "ReportColumnHeader",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_CreatedBy",
                table: "ReportColumnFilter",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_Entity",
                table: "ReportColumnFilter",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_LastModifiedBy",
                table: "ReportColumnFilter",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_CreatedBy",
                table: "ReportColumnDetail",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_Entity",
                table: "ReportColumnDetail",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_LastModifiedBy",
                table: "ReportColumnDetail",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Report_CreatedBy",
                table: "Report",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Entity",
                table: "Report",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Report_LastModifiedBy",
                table: "Report",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MainModulePlaceHolder_CreatedBy",
                table: "MainModulePlaceHolder",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MainModulePlaceHolder_LastModifiedBy",
                table: "MainModulePlaceHolder",
                column: "LastModifiedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReportTableJoinParameter_CreatedBy",
                table: "ReportTableJoinParameter");

            migrationBuilder.DropIndex(
                name: "IX_ReportTableJoinParameter_Entity",
                table: "ReportTableJoinParameter");

            migrationBuilder.DropIndex(
                name: "IX_ReportTableJoinParameter_LastModifiedBy",
                table: "ReportTableJoinParameter");

            migrationBuilder.DropIndex(
                name: "IX_ReportTable_CreatedBy",
                table: "ReportTable");

            migrationBuilder.DropIndex(
                name: "IX_ReportTable_Entity",
                table: "ReportTable");

            migrationBuilder.DropIndex(
                name: "IX_ReportTable_LastModifiedBy",
                table: "ReportTable");

            migrationBuilder.DropIndex(
                name: "IX_ReportRoleAssignment_CreatedBy",
                table: "ReportRoleAssignment");

            migrationBuilder.DropIndex(
                name: "IX_ReportRoleAssignment_Entity",
                table: "ReportRoleAssignment");

            migrationBuilder.DropIndex(
                name: "IX_ReportRoleAssignment_LastModifiedBy",
                table: "ReportRoleAssignment");

            migrationBuilder.DropIndex(
                name: "IX_ReportQueryFilter_CreatedBy",
                table: "ReportQueryFilter");

            migrationBuilder.DropIndex(
                name: "IX_ReportQueryFilter_Entity",
                table: "ReportQueryFilter");

            migrationBuilder.DropIndex(
                name: "IX_ReportQueryFilter_LastModifiedBy",
                table: "ReportQueryFilter");

            migrationBuilder.DropIndex(
                name: "IX_ReportFilterGrouping_CreatedBy",
                table: "ReportFilterGrouping");

            migrationBuilder.DropIndex(
                name: "IX_ReportFilterGrouping_Entity",
                table: "ReportFilterGrouping");

            migrationBuilder.DropIndex(
                name: "IX_ReportFilterGrouping_LastModifiedBy",
                table: "ReportFilterGrouping");

            migrationBuilder.DropIndex(
                name: "IX_ReportColumnHeader_CreatedBy",
                table: "ReportColumnHeader");

            migrationBuilder.DropIndex(
                name: "IX_ReportColumnHeader_Entity",
                table: "ReportColumnHeader");

            migrationBuilder.DropIndex(
                name: "IX_ReportColumnHeader_LastModifiedBy",
                table: "ReportColumnHeader");

            migrationBuilder.DropIndex(
                name: "IX_ReportColumnFilter_CreatedBy",
                table: "ReportColumnFilter");

            migrationBuilder.DropIndex(
                name: "IX_ReportColumnFilter_Entity",
                table: "ReportColumnFilter");

            migrationBuilder.DropIndex(
                name: "IX_ReportColumnFilter_LastModifiedBy",
                table: "ReportColumnFilter");

            migrationBuilder.DropIndex(
                name: "IX_ReportColumnDetail_CreatedBy",
                table: "ReportColumnDetail");

            migrationBuilder.DropIndex(
                name: "IX_ReportColumnDetail_Entity",
                table: "ReportColumnDetail");

            migrationBuilder.DropIndex(
                name: "IX_ReportColumnDetail_LastModifiedBy",
                table: "ReportColumnDetail");

            migrationBuilder.DropIndex(
                name: "IX_Report_CreatedBy",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_Entity",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_LastModifiedBy",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_MainModulePlaceHolder_CreatedBy",
                table: "MainModulePlaceHolder");

            migrationBuilder.DropIndex(
                name: "IX_MainModulePlaceHolder_LastModifiedBy",
                table: "MainModulePlaceHolder");

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportTableJoinParameter",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportRoleAssignment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportQueryFilter",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportFilterGrouping",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportColumnHeader",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportColumnFilter",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "ReportColumnDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "Report",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "MainModulePlaceHolder",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);
        }
    }
}
