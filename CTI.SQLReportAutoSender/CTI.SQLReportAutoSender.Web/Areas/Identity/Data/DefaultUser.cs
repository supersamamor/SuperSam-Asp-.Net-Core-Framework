using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CTI.SQLReportAutoSender.Core.Identity;
using CTI.SQLReportAutoSender.Infrastructure.Data;

namespace CTI.SQLReportAutoSender.Web.Areas.Identity.Data;

public static class DefaultUser
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        using var context = new IdentityContext(
            serviceProvider.GetRequiredService<DbContextOptions<IdentityContext>>());
        var entity = await context.Entities.FirstAsync(e => e.Name == "Default");
        var user = new ApplicationUser
        {
            UserName = WebConstants.AdminEmail,
            Email = WebConstants.AdminEmail,
            Name = WebConstants.AdminFullName,
            BirthDate = DateTime.MinValue,
            EntityId = entity.Id,
            IsActive = true,
            EmailConfirmed = true,
        };
        var exists = await userManager.FindByEmailAsync(user.Email);
        if (exists == null)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            _ = await userManager.CreateAsync(user, configuration.GetValue<string>("DefaultPassword"));
            _ = await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
