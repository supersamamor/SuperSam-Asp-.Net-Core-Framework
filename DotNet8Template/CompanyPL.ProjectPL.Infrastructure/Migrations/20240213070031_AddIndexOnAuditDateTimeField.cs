using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPL.ProjectPL.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexOnAuditDateTimeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_DateTime",
                table: "AuditLogs",
                column: "DateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_DateTime",
                table: "AuditLogs");
        }
    }
}
