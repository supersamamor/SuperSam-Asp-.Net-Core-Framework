using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.DSF.Infrastructure.Migrations
{
    public partial class AddEntitySetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "TaskList",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "TaskList",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "TaskList",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Team",
                table: "TaskList",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Company_CompanyCode",
                        column: x => x.CompanyCode,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SectionCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Section_Department_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "Department",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TeamCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Section_SectionCode",
                        column: x => x.SectionCode,
                        principalTable: "Section",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_CompanyCode",
                table: "Company",
                column: "CompanyCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_CompanyName",
                table: "Company",
                column: "CompanyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_Entity",
                table: "Company",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Company_LastModifiedDate",
                table: "Company",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CompanyCode",
                table: "Department",
                column: "CompanyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Department_DepartmentCode",
                table: "Department",
                column: "DepartmentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_Entity",
                table: "Department",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Department_LastModifiedDate",
                table: "Department",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Section_DepartmentCode",
                table: "Section",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Section_Entity",
                table: "Section",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Section_LastModifiedDate",
                table: "Section",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Section_SectionCode",
                table: "Section",
                column: "SectionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_Entity",
                table: "Team",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_Team_LastModifiedDate",
                table: "Team",
                column: "LastModifiedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Team_SectionCode",
                table: "Team",
                column: "SectionCode");

            migrationBuilder.CreateIndex(
                name: "IX_Team_TeamCode",
                table: "Team",
                column: "TeamCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "Team",
                table: "TaskList");
        }
    }
}
