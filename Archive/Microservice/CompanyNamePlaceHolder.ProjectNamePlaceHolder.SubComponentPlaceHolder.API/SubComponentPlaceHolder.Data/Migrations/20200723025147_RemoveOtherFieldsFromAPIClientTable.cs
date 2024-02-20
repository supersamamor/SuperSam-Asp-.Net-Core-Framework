using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SubComponentPlaceHolder.Data.Migrations
{
    public partial class RemoveOtherFieldsFromAPIClientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MainModulePlaceHolder_MainModulePlaceHolderId",
                table: "MainModulePlaceHolder");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "SubComponentPlaceHolderApiClient");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "SubComponentPlaceHolderApiClient");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SubComponentPlaceHolderApiClient");

            migrationBuilder.DropColumn(
                name: "Secret",
                table: "SubComponentPlaceHolderApiClient");

            migrationBuilder.DropColumn(
                name: "MainModulePlaceHolderId",
                table: "MainModulePlaceHolder");

            migrationBuilder.AddColumn<DateTime>(
                name: "Expiry",
                table: "SubComponentPlaceHolderApiClient",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "SubComponentPlaceHolderApiClient",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expiry",
                table: "SubComponentPlaceHolderApiClient");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "SubComponentPlaceHolderApiClient");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "SubComponentPlaceHolderApiClient",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "SubComponentPlaceHolderApiClient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SubComponentPlaceHolderApiClient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "SubComponentPlaceHolderApiClient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainModulePlaceHolderId",
                table: "MainModulePlaceHolder",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainModulePlaceHolder_MainModulePlaceHolderId",
                table: "MainModulePlaceHolder",
                column: "MainModulePlaceHolderId",
                unique: true,
                filter: "[MainModulePlaceHolderId] IS NOT NULL");
        }
    }
}
