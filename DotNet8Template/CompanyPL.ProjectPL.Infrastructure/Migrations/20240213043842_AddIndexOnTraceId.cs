using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPL.ProjectPL.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexOnTraceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TraceId",
                table: "AuditLogs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_TraceId",
                table: "AuditLogs",
                column: "TraceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_TraceId",
                table: "AuditLogs");

            migrationBuilder.AlterColumn<string>(
                name: "TraceId",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
