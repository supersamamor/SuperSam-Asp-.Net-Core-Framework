using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.ELMS.Infrastructure.Migrations
{
    public partial class AddOffersheetPerProjectCounterUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Offering_OfferSheetPerProjectCounter_ProjectID",
                table: "Offering",
                columns: new[] { "OfferSheetPerProjectCounter", "ProjectID" },
                unique: true,
                filter: "[OfferSheetPerProjectCounter] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Offering_OfferSheetPerProjectCounter_ProjectID",
                table: "Offering");
        }
    }
}
