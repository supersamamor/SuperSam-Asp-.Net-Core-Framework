using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectNamePlaceHolder.Data.Migrations
{
    public partial class InitialDatabaseSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainModulePlaceHolder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedByUsername = table.Column<string>(nullable: true),
                    UpdatedByUsername = table.Column<string>(nullable: true),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainModulePlaceHolder", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainModulePlaceHolder_Code",
                table: "MainModulePlaceHolder",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainModulePlaceHolder");
        }
    }
}
