using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
{
    public partial class AddFieldOnCompanyForCheckPrepare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryCorporationBranch",
                table: "Company",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Signatory1",
                table: "Company",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Signatory2",
                table: "Company",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignatoryType",
                table: "Company",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryCorporationBranch",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Signatory1",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Signatory2",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "SignatoryType",
                table: "Company");
        }
    }
}
