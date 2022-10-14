using CTI.Common.Identity.Abstractions;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Web.Areas.Identity.Data;

public static class DefaultApprovalSetup
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>(),
            serviceProvider.GetRequiredService<IAuthenticatedUser>());
        var entity = await context.ApproverSetup.FirstOrDefaultAsync(e =>
                e.TableName == ApprovalModule.Report && e.ApprovalSetupType == ApprovalSetupTypes.Modular);
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var adminRole = await roleManager.FindByNameAsync(WebConstants.AdminRole);
        if (entity == null)
        {
            var approvalSetup = new ApproverSetupState()
            {
                ApprovalSetupType = ApprovalSetupTypes.Modular,
                TableName = ApprovalModule.Report,
                ApprovalType = ApprovalTypes.All,
                EmailSubject = "New Report For Approval",
                EmailBody = @"<p>Hi {ApproverName},</p>
                                <p>Please  click the link below for you to approve the report.</p>
                                <p><a href=""{ApprovalUrl}"" target=""_blank"">Click here.</a></p>
                                <p>Thank you.</p>",
                Entity = "Default",
                CreatedBy = "System",
                CreatedDate = DateTime.Now,
                LastModifiedBy = "System",
                LastModifiedDate = DateTime.Now,
            };
            approvalSetup.ApproverAssignmentList = new List<ApproverAssignmentState>();
            approvalSetup.ApproverAssignmentList.Add(new ApproverAssignmentState()
            {
                ApproverType = ApproverTypes.Role,
                Sequence = 1,
                ApproverRoleId = adminRole.Id,
                Entity = "Default",
                CreatedBy = "System",
                CreatedDate = DateTime.Now,
                LastModifiedBy = "System",
                LastModifiedDate = DateTime.Now,
            });
            context.ApproverSetup.Add(approvalSetup);
            await context.SaveChangesAsync();
        }
    }
}
