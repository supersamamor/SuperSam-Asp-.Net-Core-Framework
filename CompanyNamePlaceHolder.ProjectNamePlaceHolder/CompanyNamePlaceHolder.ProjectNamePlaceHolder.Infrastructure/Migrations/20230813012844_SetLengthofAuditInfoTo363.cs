using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations
{
    public partial class SetLengthofAuditInfoTo363 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ReportTableJoinParameter_LastModifiedDate",
                table: "ReportTableJoinParameter",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTable_LastModifiedDate",
                table: "ReportTable",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignment_LastModifiedDate",
                table: "ReportRoleAssignment",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQueryFilter_LastModifiedDate",
                table: "ReportQueryFilter",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilterGrouping_LastModifiedDate",
                table: "ReportFilterGrouping",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnHeader_LastModifiedDate",
                table: "ReportColumnHeader",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnFilter_LastModifiedDate",
                table: "ReportColumnFilter",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReportColumnDetail_LastModifiedDate",
                table: "ReportColumnDetail",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Report_LastModifiedDate",
                table: "Report",
                column: "LastModifiedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReportTableJoinParameter_LastModifiedDate",
                table: "ReportTableJoinParameter");

            migrationBuilder.DropIndex(
                name: "IX_ReportTable_LastModifiedDate",
                table: "ReportTable");

            migrationBuilder.DropIndex(
                name: "IX_ReportRoleAssignment_LastModifiedDate",
                table: "ReportRoleAssignment");

            migrationBuilder.DropIndex(
                name: "IX_ReportQueryFilter_LastModifiedDate",
                table: "ReportQueryFilter");

            migrationBuilder.DropIndex(
                name: "IX_ReportFilterGrouping_LastModifiedDate",
                table: "ReportFilterGrouping");

            migrationBuilder.DropIndex(
                name: "IX_ReportColumnHeader_LastModifiedDate",
                table: "ReportColumnHeader");

            migrationBuilder.DropIndex(
                name: "IX_ReportColumnFilter_LastModifiedDate",
                table: "ReportColumnFilter");

            migrationBuilder.DropIndex(
                name: "IX_ReportColumnDetail_LastModifiedDate",
                table: "ReportColumnDetail");

            migrationBuilder.DropIndex(
                name: "IX_Report_LastModifiedDate",
                table: "Report");
        }
    }
}
