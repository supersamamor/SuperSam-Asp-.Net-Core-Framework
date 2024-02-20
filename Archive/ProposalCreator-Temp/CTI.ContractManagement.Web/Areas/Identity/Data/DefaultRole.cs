using CTI.Common.Utility.Extensions;
using LanguageExt;
using Microsoft.AspNetCore.Identity;

namespace CTI.ContractManagement.Web.Areas.Identity.Data;

public static class DefaultRole
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var adminRole = new IdentityRole("Admin");
        if (!await roleManager.RoleExistsAsync(adminRole.Name))
        {
            _ = await roleManager.CreateAsync(adminRole);
        }
        adminRole = await roleManager.FindByNameAsync(adminRole.Name);
        var result = await roleManager.AddPermissionClaims(adminRole, Permission.GenerateAllPermissions());
        result.IfFail(e => logger.LogError("Error in DefaultRole.Seed. Errors = {Errors}", e.Join().ToString()));
    }
}
