using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.DPI.Infrastructure.Migrations
{
    public partial class AddFieldsOnQueryFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportQueryFilter_Report_ReportId",
                table: "ReportQueryFilter");

            migrationBuilder.AlterColumn<string>(
                name: "ReportId",
                table: "ReportQueryFilter",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FieldName",
                table: "ReportQueryFilter",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomDropdownValues",
                table: "ReportQueryFilter",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataType",
                table: "ReportQueryFilter",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DropdownFilter",
                table: "ReportQueryFilter",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DropdownTableKeyAndValue",
                table: "ReportQueryFilter",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldDescription",
                table: "ReportQueryFilter",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "ReportQueryFilter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportQueryFilter_Report_ReportId",
                table: "ReportQueryFilter",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportQueryFilter_Report_ReportId",
                table: "ReportQueryFilter");

            migrationBuilder.DropColumn(
                name: "CustomDropdownValues",
                table: "ReportQueryFilter");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "ReportQueryFilter");

            migrationBuilder.DropColumn(
                name: "DropdownFilter",
                table: "ReportQueryFilter");

            migrationBuilder.DropColumn(
                name: "DropdownTableKeyAndValue",
                table: "ReportQueryFilter");

            migrationBuilder.DropColumn(
                name: "FieldDescription",
                table: "ReportQueryFilter");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "ReportQueryFilter");

            migrationBuilder.AlterColumn<string>(
                name: "ReportId",
                table: "ReportQueryFilter",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FieldName",
                table: "ReportQueryFilter",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportQueryFilter_Report_ReportId",
                table: "ReportQueryFilter",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id");
        }
    }
}
