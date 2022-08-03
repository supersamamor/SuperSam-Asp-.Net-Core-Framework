using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.TenantSales.Infrastructure.Migrations
{
    public partial class InitialDatabaseSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Revalidate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SalesDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProcessingRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revalidate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revalidate_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Revalidate_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Revalidate_Entity",
                table: "Revalidate",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Revalidate_LastModifiedDate",
                table: "Revalidate",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Revalidate_ProjectId",
                table: "Revalidate",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Revalidate_TenantId",
                table: "Revalidate",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Revalidate");
        }
    }
}
