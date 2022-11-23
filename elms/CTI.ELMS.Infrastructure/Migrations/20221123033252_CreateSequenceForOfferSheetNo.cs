using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.ELMS.Infrastructure.Migrations
{
    public partial class CreateSequenceForOfferSheetNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "OfferSheetNoSequence");

            migrationBuilder.AddColumn<string>(
                name: "OfferSheetId",
                table: "Offering",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");
            migrationBuilder.Sql("Update Offering Set OfferSheetId = Id");
            migrationBuilder.CreateIndex(
                name: "IX_Offering_OfferSheetId",
                table: "Offering",
                column: "OfferSheetId",
                unique: true);
            migrationBuilder.Sql(@"Declare @MaxId Int
                    Declare @CurrentValue Int
                    Set @MaxId = (SELECT Max(Convert(Int,Id))
				                      FROM [dbo].[Offering]
				                      where ISNUMERIC(id) = 1)
                    Set @CurrentValue = (SELECT Convert(Int,current_value) FROM sys.sequences WHERE name =  '" + Infrastructure.Constants.OfferSheetNoSequence + @"')

                    Select @MaxId,@CurrentValue

                    Declare @Ctr Int
                    set @Ctr = @MaxId - @CurrentValue + 1
                    While (@Ctr>0)
                    Begin
	                    SELECT RIGHT('00000' + CAST(NEXT VALUE FOR " + Infrastructure.Constants.OfferSheetNoSequence + @" AS VARCHAR), 5)
	                    Set @Ctr = @Ctr - 1
                    End
                    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Offering_OfferSheetId",
                table: "Offering");

            migrationBuilder.DropSequence(
                name: "OfferSheetNoSequence");

            migrationBuilder.DropColumn(
                name: "OfferSheetId",
                table: "Offering");
        }
    }
}
