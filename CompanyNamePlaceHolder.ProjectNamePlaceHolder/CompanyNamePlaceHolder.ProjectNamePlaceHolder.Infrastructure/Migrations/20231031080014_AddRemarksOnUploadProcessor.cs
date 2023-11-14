using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRemarksOnUploadProcessor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "UploadStaging",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "UploadProcessor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "UploadStaging");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "UploadProcessor");
        }
    }
}
