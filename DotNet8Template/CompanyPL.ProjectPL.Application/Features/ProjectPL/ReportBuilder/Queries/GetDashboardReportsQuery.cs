using CompanyPL.Common.Identity.Abstractions;
using CompanyPL.ProjectPL.Application.DTOs;
using CompanyPL.ProjectPL.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.Report.Queries;

public record GetDashboardReportsQuery() : IRequest<IList<ReportResultModel>>;
public class GetDashboardReportsQueryHandler : IRequestHandler<GetDashboardReportsQuery, IList<ReportResultModel>>
{
    private readonly ApplicationContext _context;
    private readonly IConfiguration _configuration;
    private readonly IdentityContext _identityContext;
    private readonly IAuthenticatedUser _authenticatedUser;
    public GetDashboardReportsQueryHandler(ApplicationContext context, IConfiguration configuration, IdentityContext identityContext, IAuthenticatedUser authenticatedUser)
    {
        _context = context;
        _configuration = configuration;
        _identityContext = identityContext;
        _authenticatedUser = authenticatedUser;
    }

    public async Task<IList<ReportResultModel>> Handle(GetDashboardReportsQuery request, CancellationToken cancellationToken = default)
    {
        var roleList = await (from ur in _identityContext.UserRoles
                              join r in _identityContext.Roles on ur.RoleId equals r.Id
                              where ur.UserId == _authenticatedUser.UserId
                              select r.Name).Distinct().ToListAsync(cancellationToken);
        var reportList = await _context.Report         
            .Include(l => l.ReportQueryFilterList)
            .Where(e => e.DisplayOnDashboard == true).AsNoTracking()
            .Where(l => l.ReportRoleAssignmentList!.Any(ra => roleList.Contains(ra.RoleName)))
            .OrderBy(l => l.Sequence).ToListAsync(cancellationToken);
        IList<ReportResultModel> reportResult = new List<ReportResultModel>();
        foreach (var report in reportList)
        {
            var filters = new List<ReportQueryFilterModel>();
            if (report?.ReportQueryFilterList?.Count > 0)
            {
                foreach (var parameter in report.ReportQueryFilterList)
                {
                    filters.Add(new ReportQueryFilterModel() { FieldName = parameter.FieldName! });
                }
            }
            var resultsAndLabels = await Helpers.ReportDataHelper.ConvertSQLQueryToJsonAsync(_authenticatedUser, _configuration.GetConnectionString("ReportContext"), report!, filters);
            reportResult.Add(new ReportResultModel()
            {
                ReportId = report?.Id,
                ReportName = report!.ReportName,
                Results = resultsAndLabels.Results,
                ColumnHeaders = resultsAndLabels.ColumnHeaders,      
                ReportOrChartType = report!.ReportOrChartType,
                DisplayLegend = resultsAndLabels.DisplayLegend
            });
        }
        return reportResult;
    }

}
