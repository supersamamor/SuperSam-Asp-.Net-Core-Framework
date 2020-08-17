using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Data.Migrations
{
    public partial class RemoveOtherFieldsFromAPIClientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ProjectNamePlaceHolderIdentityApiClient");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "ProjectNamePlaceHolderIdentityApiClient");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProjectNamePlaceHolderIdentityApiClient");

            migrationBuilder.DropColumn(
                name: "Secret",
                table: "ProjectNamePlaceHolderIdentityApiClient");

            migrationBuilder.AddColumn<DateTime>(
                name: "Expiry",
                table: "ProjectNamePlaceHolderIdentityApiClient",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "ProjectNamePlaceHolderIdentityApiClient",
                nullable: true);

            migrationBuilder.Sql(@"truncate table ProjectNamePlaceHolderIdentityApiClient
                                insert into ProjectNamePlaceHolderIdentityApiClient(Token) values(newid())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expiry",
                table: "ProjectNamePlaceHolderIdentityApiClient");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "ProjectNamePlaceHolderIdentityApiClient");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ProjectNamePlaceHolderIdentityApiClient",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "ProjectNamePlaceHolderIdentityApiClient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProjectNamePlaceHolderIdentityApiClient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "ProjectNamePlaceHolderIdentityApiClient",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
