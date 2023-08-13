using CompanyNamePlaceHolder.Common.Identity.Abstractions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;

public static class DefaultDashboard
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>(), serviceProvider.GetRequiredService<IAuthenticatedUser>());
        var entity = await context.Report.FirstOrDefaultAsync(e => e.ReportName == "Activity Logs - Horizontal Bar Graph");
        if (entity == null)
        {
            context.Report.Add(new ReportState()
            {
                Id = new Guid().ToString(),
                ReportName = "Activity Logs - Horizontal Bar Graph",
                QueryType = Core.Constants.QueryType.TSql,
                ReportOrChartType = Core.Constants.ReportChartType.HorizontalBar,
                IsDistinct = false,
                QueryString = @"SELECT
                                  [Type] [Label]
                                 ,count(*) [Data]
                              FROM [dbo].[AuditLogs]
                              group by [Type]",
                DisplayOnDashboard = true,
                DisplayOnReportModule = false,
                Sequence = 1,
                ReportRoleAssignmentList = new List<ReportRoleAssignmentState>() {
                    new ReportRoleAssignmentState()
                    {
                        Id = new Guid().ToString(),
                        RoleName = Core.Constants.Roles.Admin
                    }
                }
            });
            context.Report.Add(new ReportState()
            {
                Id = new Guid().ToString(),
                ReportName = "Activity Logs - Pie Chart",
                QueryType = Core.Constants.QueryType.TSql,
                ReportOrChartType = Core.Constants.ReportChartType.Pie,
                IsDistinct = false,
                QueryString = @"SELECT
                                  [Type] [Label]
                                 ,count(*) [Data]
                              FROM [dbo].[AuditLogs]
                              group by [Type]",
                DisplayOnDashboard = true,
                DisplayOnReportModule = false,
                Sequence = 2,
                ReportRoleAssignmentList = new List<ReportRoleAssignmentState>() {
                    new ReportRoleAssignmentState()
                    {
                        Id = new Guid().ToString(),
                        RoleName = Core.Constants.Roles.Admin
                    }
                }
            });
            context.Report.Add(new ReportState()
            {
                Id = new Guid().ToString(),
                ReportName = "Activity Logs - Table",
                QueryType = Core.Constants.QueryType.TSql,
                ReportOrChartType = Core.Constants.ReportChartType.Table,
                IsDistinct = false,
                QueryString = @"SELECT
                              [Type] [Activity]
                             ,count(*) [Count]
                          FROM [dbo].[AuditLogs]
                          group by [Type]",
                DisplayOnDashboard = true,
                DisplayOnReportModule = true,
                Sequence = 3,
                ReportRoleAssignmentList = new List<ReportRoleAssignmentState>() {
                    new ReportRoleAssignmentState()
                    {
                        Id = new Guid().ToString(),
                        RoleName = Core.Constants.Roles.Admin
                    }
                }
            });
            await context.SaveChangesAsync();
        }
    }
}
