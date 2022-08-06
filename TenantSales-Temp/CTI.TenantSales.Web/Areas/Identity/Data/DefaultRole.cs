using CTI.Common.Utility.Extensions;
using LanguageExt;
using Microsoft.AspNetCore.Identity;

namespace CTI.TenantSales.Web.Areas.Identity.Data;

public static class DefaultRole
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var adminRole = new IdentityRole(Roles.Admin);
        if (!await roleManager.RoleExistsAsync(adminRole.Name))
        {
            _ = await roleManager.CreateAsync(adminRole);
        }
        adminRole = await roleManager.FindByNameAsync(adminRole.Name);
        var result = await roleManager.AddPermissionClaims(adminRole, Permission.GenerateAllPermissions());
        result.IfFail(e => logger.LogError("Error in DefaultRole.Seed. Errors = {Errors}", e.Join().ToString()));

        var userRole = new IdentityRole(Roles.User);
        if (!await roleManager.RoleExistsAsync(userRole.Name))
        {
            _ = await roleManager.CreateAsync(userRole);
        }
        userRole = await roleManager.FindByNameAsync(userRole.Name);
        var userRolePermissionList = new List<string>()
        {
            Permission.MasterFile.View,
            Permission.TenantPOSSales.View,
            Permission.TenantPOSSales.Edit,
            Permission.TenantPOSSales.Export,
            Permission.Tenant.View,
            Permission.Tenant.Edit,
            Permission.Reports.View,
            Permission.Reports.DailySales,
            Permission.Reports.SalesGrowth,
            Permission.Project.View,
            Permission.Level.View,
            Permission.SalesCategory.View,
            Permission.TenantPOS.View,
        };

        result = await roleManager.AddPermissionClaims(userRole, userRolePermissionList);
        result.IfFail(e => logger.LogError("Error in DefaultRole.Seed. Errors = {Errors}", e.Join().ToString()));
    }
}
