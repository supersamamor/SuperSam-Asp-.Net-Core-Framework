using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Data.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Template_Code",
                table: "Template",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
            migrationBuilder.Sql("Insert Into dbo.Template (Code, Name) Values ('COD1','Name 1')");
            migrationBuilder.Sql("Insert Into dbo.Template (Code, Name) Values ('COD2','Name 2')");
            migrationBuilder.Sql("Insert Into dbo.Template (Code, Name) Values ('COD3','Name 3')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Template");
        }
    }
}
