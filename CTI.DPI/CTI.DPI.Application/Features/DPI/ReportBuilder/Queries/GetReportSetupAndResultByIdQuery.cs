using CTI.Common.Identity.Abstractions;
using CTI.DPI.Application.DTOs;
using CTI.DPI.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CTI.DPI.Application.Features.DPI.Report.Queries;

public record GetReportSetupAndResultByIdQuery(string Id) : IRequest<Option<ReportResultModel>>
{
    public IList<ReportQueryFilterModel> Filters { get; set; } = new List<ReportQueryFilterModel>();
}

public class GetReportBuilderByIdQueryHandler : IRequestHandler<GetReportSetupAndResultByIdQuery, Option<ReportResultModel>>
{
    private readonly ApplicationContext _context;
    private readonly IConfiguration _configuration;
    private readonly IdentityContext _identityContext;
    private readonly IAuthenticatedUser _authenticatedUser;
    public GetReportBuilderByIdQueryHandler(ApplicationContext context, IConfiguration configuration, IdentityContext identityContext, IAuthenticatedUser authenticatedUser)
    {
        _context = context;
        _configuration = configuration;
        _identityContext = identityContext;
        _authenticatedUser = authenticatedUser;
    }

    public async Task<Option<ReportResultModel>> Handle(GetReportSetupAndResultByIdQuery request, CancellationToken cancellationToken = default)
    {
        var roleList = await (from ur in _identityContext.UserRoles
                              join r in _identityContext.Roles on ur.RoleId equals r.Id
                              where ur.UserId == _authenticatedUser.UserId
                              select r.Name).Distinct().ToListAsync(cancellationToken);
        var report = await _context.Report
            .Include(l => l.ReportTableList)
            .Include(l => l.ReportTableJoinParameterList)
            .Include(l => l.ReportColumnHeaderList)
            .Include(l => l.ReportFilterGroupingList)
            .Include(l => l.ReportQueryFilterList)
            .Where(e => e.Id == request.Id)
            .Where(l => l.ReportRoleAssignmentList!.Any(ra => roleList.Contains(ra.RoleName)))
            .AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        if (request.Filters == null || request.Filters.Count == 0)
        {
            request.Filters = new List<ReportQueryFilterModel>();
            if (report?.ReportQueryFilterList?.Count > 0)
            {
                foreach (var parameter in report.ReportQueryFilterList)
                {
                    request.Filters.Add(new ReportQueryFilterModel() 
                    {
                        FieldName = parameter.FieldName!,
                        FieldDescription = parameter.FieldDescription!,
                        DataType = parameter.DataType!,
                        CustomDropdownValues = parameter.CustomDropdownValues!,
                        DropdownTableKeyAndValue = parameter.DropdownTableKeyAndValue!,
                        DropdownFilter = parameter.DropdownFilter!,
                        Sequence = parameter.Sequence!,
                    });
                }
            }
        }
        var resultsAndLabels = await Helpers.ReportDataHelper.ConvertSQLQueryToJsonAsync(
            _authenticatedUser,
            _configuration.GetConnectionString("ReportContext"),
            report!,
            request.Filters);
        return new ReportResultModel()
        {
            ReportId = request.Id,
            ReportName = report!.ReportName,
            Results = resultsAndLabels.Results,
            Labels = resultsAndLabels.Labels,
            Colors = resultsAndLabels.Colors,
            ReportOrChartType = report!.ReportOrChartType,
            Filters = request.Filters,
        };
    }

}
