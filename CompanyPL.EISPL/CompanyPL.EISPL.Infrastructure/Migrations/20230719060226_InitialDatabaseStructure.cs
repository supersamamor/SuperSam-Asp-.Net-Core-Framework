using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPL.EISPL.Infrastructure.Migrations
{
    public partial class InitialDatabaseStructure : Migration
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
                    PrimaryKey = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    TraceId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PLEmployee",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PLFirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PLMiddleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PLEmployeeCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PLLastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLEmployee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PLContactInformation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PLContactDetails = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PLEmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLContactInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PLContactInformation_PLEmployee_PLEmployeeId",
                        column: x => x.PLEmployeeId,
                        principalTable: "PLEmployee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PLHealthDeclaration",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PLVaccine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PLIsVaccinated = table.Column<bool>(type: "bit", nullable: true),
                    PLEmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLHealthDeclaration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PLHealthDeclaration_PLEmployee_PLEmployeeId",
                        column: x => x.PLEmployeeId,
                        principalTable: "PLEmployee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PLEmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TestColumn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test_PLEmployee_PLEmployeeId",
                        column: x => x.PLEmployeeId,
                        principalTable: "PLEmployee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_PrimaryKey",
                table: "AuditLogs",
                column: "PrimaryKey");

            migrationBuilder.CreateIndex(
                name: "IX_PLContactInformation_Entity",
                table: "PLContactInformation",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_PLContactInformation_LastModifiedDate",
                table: "PLContactInformation",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_PLContactInformation_PLEmployeeId",
                table: "PLContactInformation",
                column: "PLEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PLEmployee_Entity",
                table: "PLEmployee",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_PLEmployee_LastModifiedDate",
                table: "PLEmployee",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_PLEmployee_PLEmployeeCode",
                table: "PLEmployee",
                column: "PLEmployeeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PLHealthDeclaration_Entity",
                table: "PLHealthDeclaration",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_PLHealthDeclaration_LastModifiedDate",
                table: "PLHealthDeclaration",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_PLHealthDeclaration_PLEmployeeId",
                table: "PLHealthDeclaration",
                column: "PLEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Entity",
                table: "Test",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Test_LastModifiedDate",
                table: "Test",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Test_PLEmployeeId",
                table: "Test",
                column: "PLEmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "PLContactInformation");

            migrationBuilder.DropTable(
                name: "PLHealthDeclaration");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "PLEmployee");
        }
    }
}
