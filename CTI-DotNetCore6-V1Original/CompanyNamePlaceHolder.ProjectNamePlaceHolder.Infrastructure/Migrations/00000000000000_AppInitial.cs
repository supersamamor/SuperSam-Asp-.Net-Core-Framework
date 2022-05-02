using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations;

public partial class AppInitial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AuditLogs",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                TraceId = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AuditLogs", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "MainModulePlaceHolder",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Status = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MainModulePlaceHolder", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_MainModulePlaceHolder_Code",
            table: "MainModulePlaceHolder",
            column: "Code");

        migrationBuilder.CreateIndex(
            name: "IX_MainModulePlaceHolder_Entity",
            table: "MainModulePlaceHolder",
            column: "Entity");

        migrationBuilder.CreateIndex(
            name: "IX_MainModulePlaceHolder_LastModifiedDate",
            table: "MainModulePlaceHolder",
            column: "LastModifiedDate");

        migrationBuilder.CreateIndex(
            name: "IX_MainModulePlaceHolder_Name",
            table: "MainModulePlaceHolder",
            column: "Name");

        migrationBuilder.CreateIndex(
            name: "IX_MainModulePlaceHolder_Status",
            table: "MainModulePlaceHolder",
            column: "Status");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AuditLogs");

        migrationBuilder.DropTable(
            name: "MainModulePlaceHolder");
    }
}
