using Microsoft.EntityFrameworkCore.Migrations;

namespace SubComponentPlaceHolder.Data.Migrations
{
    public partial class AddUniqueGuidOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainModulePlaceHolderId",
                table: "MainModulePlaceHolder",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainModulePlaceHolder_MainModulePlaceHolderId",
                table: "MainModulePlaceHolder",
                column: "MainModulePlaceHolderId",
                unique: true,
                filter: "[MainModulePlaceHolderId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MainModulePlaceHolder_MainModulePlaceHolderId",
                table: "MainModulePlaceHolder");

            migrationBuilder.DropColumn(
                name: "MainModulePlaceHolderId",
                table: "MainModulePlaceHolder");
        }
    }
}
