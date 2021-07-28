using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations
{
    public partial class InitialDatabaseSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainModulePlaceHolder",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainModulePlaceHolder", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainModulePlaceHolder_Entity",
                table: "MainModulePlaceHolder",
                column: "Entity");

            migrationBuilder.CreateIndex(
                name: "IX_MainModulePlaceHolder_LastModifiedDate",
                table: "MainModulePlaceHolder",
                column: "LastModifiedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainModulePlaceHolder");
        }
    }
}
