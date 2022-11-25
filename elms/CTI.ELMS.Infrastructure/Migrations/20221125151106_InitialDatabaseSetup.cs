using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.ELMS.Infrastructure.Migrations
{
    public partial class InitialDatabaseSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        }
    }
}
