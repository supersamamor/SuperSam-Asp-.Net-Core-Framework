using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Data.Migrations
{
    public partial class AddTableApiClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectNamePlaceHolderIdentityApiClient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Secret = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectNamePlaceHolderIdentityApiClient", x => x.Id);
                });

            migrationBuilder.Sql(@"Insert Into [dbo].[ProjectNamePlaceHolderIdentityApiClient] ([Name],[Key],Secret,Active)
                                    Values ('ProjectNamePlaceHolder Web',newid(),newid(),1);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectNamePlaceHolderIdentityApiClient");
        }
    }
}
