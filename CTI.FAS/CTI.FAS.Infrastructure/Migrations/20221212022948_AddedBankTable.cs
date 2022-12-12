using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.FAS.Infrastructure.Migrations
{
    public partial class AddedBankTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "BankCode",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Company");

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

            migrationBuilder.AddColumn<string>(
                name: "BankId",
                table: "PaymentTransaction",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BankCode = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    DeliveryCorporationBranch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SignatoryType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Signatory1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Signatory2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bank_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_BankId",
                table: "PaymentTransaction",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_CompanyId",
                table: "Bank",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransaction_Bank_BankId",
                table: "PaymentTransaction",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransaction_Bank_BankId",
                table: "PaymentTransaction");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransaction_BankId",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "PaymentTransaction");

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "Company",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Company",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "Company",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankCode",
                table: "Company",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Company",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

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
    }
}
