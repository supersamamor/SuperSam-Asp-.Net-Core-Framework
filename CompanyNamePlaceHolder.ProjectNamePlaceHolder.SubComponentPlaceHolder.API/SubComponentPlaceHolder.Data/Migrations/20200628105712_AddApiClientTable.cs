using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SubComponentPlaceHolder.Data.Migrations
{
    public partial class AddApiClientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubComponentPlaceHolderApiClient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedByUsername = table.Column<string>(nullable: true),
                    UpdatedByUsername = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Secret = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubComponentPlaceHolderApiClient", x => x.Id);
                });
            migrationBuilder.Sql(@"Insert Into [dbo].[SubComponentPlaceHolderApiClient] ([Name],[Key],Secret,Active,CreatedDate,UpdatedDate,CreatedByUsername,UpdatedByUsername)
                                    Values ('ProjectNamePlaceHolder Web',newid(),newid(),1,GETUTCDATE(),GETUTCDATE(),'SYSTEM','SYSTEM');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubComponentPlaceHolderApiClient");
        }
    }
}
