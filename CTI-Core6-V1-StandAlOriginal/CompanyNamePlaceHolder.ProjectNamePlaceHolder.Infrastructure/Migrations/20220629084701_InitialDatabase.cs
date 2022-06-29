using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations
{
    public partial class InitialDatabase : Migration
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
                name: "MainModulePlaceHolder",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "SubDetailItemPlaceHolder",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MainModulePlaceHolderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubDetailItemPlaceHolder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubDetailItemPlaceHolder_MainModulePlaceHolder_MainModulePlaceHolderId",
                        column: x => x.MainModulePlaceHolderId,
                        principalTable: "MainModulePlaceHolder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubDetailListPlaceHolder",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MainModulePlaceHolderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubDetailListPlaceHolder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubDetailListPlaceHolder_MainModulePlaceHolder_MainModulePlaceHolderId",
                        column: x => x.MainModulePlaceHolderId,
                        principalTable: "MainModulePlaceHolder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_PrimaryKey",
                table: "AuditLogs",
                column: "PrimaryKey");

            migrationBuilder.CreateIndex(
                name: "IX_MainModulePlaceHolder_Code",
                table: "MainModulePlaceHolder",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainModulePlaceHolder_Entity",
                table: "MainModulePlaceHolder",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_MainModulePlaceHolder_LastModifiedDate",
                table: "MainModulePlaceHolder",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_SubDetailItemPlaceHolder_Code",
                table: "SubDetailItemPlaceHolder",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubDetailItemPlaceHolder_Entity",
                table: "SubDetailItemPlaceHolder",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_SubDetailItemPlaceHolder_LastModifiedDate",
                table: "SubDetailItemPlaceHolder",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_SubDetailItemPlaceHolder_MainModulePlaceHolderId",
                table: "SubDetailItemPlaceHolder",
                column: "MainModulePlaceHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_SubDetailListPlaceHolder_Code",
                table: "SubDetailListPlaceHolder",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubDetailListPlaceHolder_Entity",
                table: "SubDetailListPlaceHolder",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_SubDetailListPlaceHolder_LastModifiedDate",
                table: "SubDetailListPlaceHolder",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_SubDetailListPlaceHolder_MainModulePlaceHolderId",
                table: "SubDetailListPlaceHolder",
                column: "MainModulePlaceHolderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "SubDetailItemPlaceHolder");

            migrationBuilder.DropTable(
                name: "SubDetailListPlaceHolder");

            migrationBuilder.DropTable(
                name: "MainModulePlaceHolder");
        }
    }
}
