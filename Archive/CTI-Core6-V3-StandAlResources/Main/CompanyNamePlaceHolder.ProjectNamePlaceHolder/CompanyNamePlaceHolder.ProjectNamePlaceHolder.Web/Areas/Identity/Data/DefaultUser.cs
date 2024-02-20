using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Identity;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;

public static class DefaultUser
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        using var context = new IdentityContext(
            serviceProvider.GetRequiredService<DbContextOptions<IdentityContext>>());
        var entity = await context.Entities.FirstAsync(e => e.Name ==  Core.Constants.Entities.Default);
        var user = new ApplicationUser
        {
            UserName = "system@admin",
            Email = "system@admin",
            Name = "System Admin",         
            EntityId = entity.Id,
            IsActive = true,
            EmailConfirmed = true,
        };
        var exists = await userManager.FindByEmailAsync(user.Email);
        if (exists == null)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            _ = await userManager.CreateAsync(user, configuration.GetValue<string>("DefaultPassword"));
            _ = await userManager.AddToRoleAsync(user, Core.Constants.Roles.Admin);
        }
    }
}
