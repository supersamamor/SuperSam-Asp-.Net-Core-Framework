using CTI.DPI.Application.DTOs;
using CTI.DPI.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CTI.DPI.Application.Features.DPI.Report.Queries;

public record GetActiveReportBuildersQuery() : IRequest<IList<ReportResultModel>>;
public class GetActiveReportBuildersQueryHandler : IRequestHandler<GetActiveReportBuildersQuery, IList<ReportResultModel>>
{
    private readonly ApplicationContext _context;
    public GetActiveReportBuildersQueryHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IList<ReportResultModel>> Handle(GetActiveReportBuildersQuery request, CancellationToken cancellationToken = default)
    {
        var reportList = await _context.Report
            .Include(l => l.ReportTableList)
            .Include(l => l.ReportTableJoinParameterList)
            .Include(l => l.ReportColumnHeaderList)
            .Include(l => l.ReportFilterGroupingList)
            .Include(l => l.ReportQueryFilterList)
            .Where(e => e.DisplayOnDashboard == true).AsNoTracking().OrderBy(l => l.Sequence).ToListAsync(cancellationToken);
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
            var resultsAndLabels = await Helpers.ReportDataHelper.ConvertSQLQueryToJsonAsync(_context.Database.GetConnectionString()!, report!, filters);
            reportResult.Add(new ReportResultModel()
            {
                ReportId = report.Id,
                ReportName = report!.ReportName,
                Results = resultsAndLabels.Results,
                Labels = resultsAndLabels.Labels,
                Colors = resultsAndLabels.Colors,
                ReportOrChartType = report!.ReportOrChartType,
            });
        }
        return reportResult;
    }

}
