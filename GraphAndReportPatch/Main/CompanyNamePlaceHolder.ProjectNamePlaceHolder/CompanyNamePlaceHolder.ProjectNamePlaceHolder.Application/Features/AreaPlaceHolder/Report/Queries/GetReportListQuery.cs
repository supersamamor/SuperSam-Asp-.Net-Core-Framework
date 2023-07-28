using CompanyNamePlaceHolder.Common.Identity.Abstractions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.DTOs;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Report.Queries;

public record GetReportListQuery() : IRequest<IList<Dictionary<string, string>>>;
public class GetReportListQueryHandler : IRequestHandler<GetReportListQuery, IList<Dictionary<string, string>>>
{
    private readonly ApplicationContext _context;
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IdentityContext _identityContext;
    public GetReportListQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser, IdentityContext identityContext)
    {
        _context = context;
        _authenticatedUser = authenticatedUser;
        _identityContext = identityContext;
    }

    public async Task<IList<Dictionary<string, string>>> Handle(GetReportListQuery request, CancellationToken cancellationToken = default)
    {
        var roleList = await (from ur in _identityContext.UserRoles
                              join r in _identityContext.Roles on ur.RoleId equals r.Id
                              where ur.UserId == _authenticatedUser.UserId
                              select r.Name).Distinct().ToListAsync(cancellationToken);
        var reportList = await _context.Report
            .Include(l => l.ReportRoleAssignmentList).AsNoTracking()
            .Where(l => l.ReportRoleAssignmentList!.Any(ra => roleList.Contains(ra.RoleName)))
            .OrderBy(l => l.Sequence).ToListAsync(cancellationToken: cancellationToken);
        return reportList.Select(report => new Dictionary<string, string>
        {
            { "ReportId", report.Id },
            { "ReportName", report.ReportName },
            { "ReportClass", Helpers.StringHelper.Sanitize(report.ReportName) },
        }).ToList();
    }

}
