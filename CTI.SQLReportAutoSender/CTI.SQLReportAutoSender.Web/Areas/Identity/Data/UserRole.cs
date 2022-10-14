using CTI.Common.Utility.Extensions;
using LanguageExt;
using Microsoft.AspNetCore.Identity;

namespace CTI.SQLReportAutoSender.Web.Areas.Identity.Data;

public static class UserRole
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userRole = new IdentityRole(Core.Constants.Roles.User);
        if (!await roleManager.RoleExistsAsync(userRole.Name))
        {
            _ = await roleManager.CreateAsync(userRole);
        }
        userRole = await roleManager.FindByNameAsync(userRole.Name);
        var userPermissions = new List<string>()
            {
                Permission.Report.Create,
                Permission.Report.View,
                Permission.Report.Edit,
                Permission.ReportInbox.View,  
            };
        var result = await roleManager.AddPermissionClaims(userRole, userPermissions);
        result.IfFail(e => logger.LogError("Error in DefaultRole.Seed. Errors = {Errors}", e.Join().ToString()));
    }
}
