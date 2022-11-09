using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.ELMS.Infrastructure.Migrations
{
    public partial class AddDateFieldsOnLead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LatestUpdatedByUsername",
                table: "Lead",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LatestUpdatedDate",
                table: "Lead",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatestUpdatedByUsername",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "LatestUpdatedDate",
                table: "Lead");
        }
    }
}
