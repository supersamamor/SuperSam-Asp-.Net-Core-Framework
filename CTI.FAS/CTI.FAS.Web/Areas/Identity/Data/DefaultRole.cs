using CTI.Common.Utility.Extensions;
using LanguageExt;
using Microsoft.AspNetCore.Identity;

namespace CTI.FAS.Web.Areas.Identity.Data;

public static class DefaultRole
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //Insert Admin
        var adminRole = new IdentityRole(Core.Constants.Roles.Admin);
        if (!await roleManager.RoleExistsAsync(adminRole.Name))
        {
            _ = await roleManager.CreateAsync(adminRole);
        }
        adminRole = await roleManager.FindByNameAsync(adminRole.Name);
        var result = await roleManager.AddPermissionClaims(adminRole, Permission.GenerateAllPermissions());
        // Insert System Admin
         var systemAdminRole = new IdentityRole(Core.Constants.Roles.SystemAdmin);
        if (!await roleManager.RoleExistsAsync(systemAdminRole.Name))
        {
            _ = await roleManager.CreateAsync(systemAdminRole);
        }
        systemAdminRole = await roleManager.FindByNameAsync(systemAdminRole.Name);
        result = await roleManager.AddPermissionClaims(systemAdminRole, Permission.GenerateAllPermissions());
        //Insert E-Settle / Check-Prepare
        var eSettleCheckPrepareRole = new IdentityRole(Core.Constants.Roles.ESettleCheckPrepare);
        if (!await roleManager.RoleExistsAsync(eSettleCheckPrepareRole.Name))
        {
            _ = await roleManager.CreateAsync(eSettleCheckPrepareRole);
        }
        eSettleCheckPrepareRole = await roleManager.FindByNameAsync(eSettleCheckPrepareRole.Name);
        var eSettleCheckPrepareRolePermissionList = new List<string>()
        {
            Permission.EnrolledPayee.View,
            Permission.EnrolledPayee.Create,
            Permission.EnrolledPayee.Edit,
            Permission.EnrolledPayee.Deactivate,
            Permission.EnrolledPayee.ReEnroll,
            Permission.PaymentTransaction.View,
            Permission.PaymentTransaction.Edit,
            Permission.PaymentTransaction.Generate,
            Permission.PaymentTransaction.Revoke,
            Permission.AccountsPayable.View,
        };
        result = await roleManager.AddPermissionClaims(eSettleCheckPrepareRole, eSettleCheckPrepareRolePermissionList);

        result.IfFail(e => logger.LogError("Error in DefaultRole.Seed. Errors = {Errors}", e.Join().ToString()));
    }
}
