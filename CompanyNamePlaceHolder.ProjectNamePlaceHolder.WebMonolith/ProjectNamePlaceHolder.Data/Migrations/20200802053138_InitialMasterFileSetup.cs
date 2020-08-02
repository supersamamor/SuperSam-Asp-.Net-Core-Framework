using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectNamePlaceHolder.Data.Migrations
{
    public partial class InitialMasterFileSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Insert Into dbo.AspNetRoles ([Id],[Name],[NormalizedName],[ConcurrencyStamp]) 
                        Values (NewId(),'" + Roles.ADMIN + @"','" + Roles.ADMIN.ToUpper() + @"',NewId())");

            //admin admin@gmail.com Admin123!@#
            migrationBuilder.Sql(@"INSERT INTO dbo.AspNetUsers ([Id],[UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash],[SecurityStamp],
                                        [ConcurrencyStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEnd],[LockoutEnabled],[AccessFailedCount])
                                    Values (newId(),'admin@gmail.com','ADMIN@GMAIL.COM','admin@gmail.com','ADMIN@GMAIL.COM',1,'AQAAAAEAACcQAAAAEONp5PXPJi0MXjmvuKBISU1k3cIexEkO6ND9x7OniXqBkDuclKhnI9MUm0Gr9nrQwQ==',
                                        'YKFXY6OTUIBSPZCWV5V3QF3WSIWBVQCW','674680d1-3586-49c3-9185-d88209575f98',null,0,0,null,1,0)");

            migrationBuilder.Sql(@"INSERT INTO dbo.ProjectNamePlaceHolderUser ([FullName],[IdentityId])
                                    Values ('Administrator',(Select Top 1 Id From dbo.AspNetUsers Where username = 'admin@gmail.com'))");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId]) SELECT (SELECT Top 1 Id FROM AspNetUsers WHERE UserName = 'admin@gmail.com'), (SELECT Top 1 Id FROM AspNetRoles WHERE Name = '" + Roles.ADMIN + @"')");

            migrationBuilder.Sql(@"Insert Into [dbo].[ProjectNamePlaceHolderApiClient] (CreatedDate,UpdatedDate,CreatedByUsername,UpdatedByUsername,Token,Expiry)
                                    Values (GETUTCDATE(),GETUTCDATE(),'SYSTEM','SYSTEM',newid(),GETUTCDATE());");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
