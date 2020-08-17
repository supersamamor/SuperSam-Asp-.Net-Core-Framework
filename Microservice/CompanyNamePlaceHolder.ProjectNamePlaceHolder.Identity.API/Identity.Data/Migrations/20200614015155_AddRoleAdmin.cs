using Identity.Core;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Data.Migrations
{
    public partial class AddRoleAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Insert Into dbo.AspNetRoles ([Id],[Name],[NormalizedName],[ConcurrencyStamp]) 
                        Values (NewId(),'" + Roles.ADMIN + @"','" + Roles.ADMIN.ToUpper() + @"',NewId())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
