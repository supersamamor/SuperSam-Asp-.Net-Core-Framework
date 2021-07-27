using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data.Migrations
{
    public partial class updateAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserEntity");

            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EntityId",
                table: "AspNetUsers",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Entities_EntityId",
                table: "AspNetUsers",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Entities_EntityId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ApplicationUserEntity",
                columns: table => new
                {
                    EntitiesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserEntity", x => new { x.EntitiesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserEntity_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserEntity_Entities_EntitiesId",
                        column: x => x.EntitiesId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserEntity_UsersId",
                table: "ApplicationUserEntity",
                column: "UsersId");
        }
    }
}
