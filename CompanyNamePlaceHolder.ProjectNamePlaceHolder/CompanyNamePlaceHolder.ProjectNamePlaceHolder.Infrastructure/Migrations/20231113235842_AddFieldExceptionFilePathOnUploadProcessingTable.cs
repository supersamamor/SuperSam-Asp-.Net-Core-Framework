using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldExceptionFilePathOnUploadProcessingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExceptionFilePath",
                table: "UploadProcessor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExceptionFilePath",
                table: "UploadProcessor");
        }
    }
}
