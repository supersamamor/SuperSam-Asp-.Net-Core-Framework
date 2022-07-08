using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Migrations
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approval_ApproverSetup_ApproverSetupId",
                table: "Approval");

            migrationBuilder.DropIndex(
                name: "IX_Approval_DataId",
                table: "Approval");

            migrationBuilder.DropColumn(
                name: "DataId",
                table: "Approval");

            migrationBuilder.RenameColumn(
                name: "ApproverSetupId",
                table: "Approval",
                newName: "ApprovalRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Approval_ApproverSetupId",
                table: "Approval",
                newName: "IX_Approval_ApprovalRecordId");

            migrationBuilder.CreateTable(
                name: "ApprovalRecord",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApproverSetupId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    DataId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalRecord_ApproverSetup_ApproverSetupId",
                        column: x => x.ApproverSetupId,
                        principalTable: "ApproverSetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_ApproverSetupId",
                table: "ApprovalRecord",
                column: "ApproverSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRecord_DataId",
                table: "ApprovalRecord",
                column: "DataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Approval_ApprovalRecord_ApprovalRecordId",
                table: "Approval",
                column: "ApprovalRecordId",
                principalTable: "ApprovalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approval_ApprovalRecord_ApprovalRecordId",
                table: "Approval");

            migrationBuilder.DropTable(
                name: "ApprovalRecord");

            migrationBuilder.RenameColumn(
                name: "ApprovalRecordId",
                table: "Approval",
                newName: "ApproverSetupId");

            migrationBuilder.RenameIndex(
                name: "IX_Approval_ApprovalRecordId",
                table: "Approval",
                newName: "IX_Approval_ApproverSetupId");

            migrationBuilder.AddColumn<string>(
                name: "DataId",
                table: "Approval",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_DataId",
                table: "Approval",
                column: "DataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Approval_ApproverSetup_ApproverSetupId",
                table: "Approval",
                column: "ApproverSetupId",
                principalTable: "ApproverSetup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
