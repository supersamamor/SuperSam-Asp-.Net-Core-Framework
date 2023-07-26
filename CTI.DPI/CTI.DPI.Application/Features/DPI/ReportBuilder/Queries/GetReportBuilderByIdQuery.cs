using CTI.DPI.Application.DTOs;
using CTI.DPI.Core.Constants;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;

namespace CTI.DPI.Application.Features.DPI.Report.Queries;

public record GetReportBuilderByIdQuery(string Id) : IRequest<Option<ReportResultModel>>
{
    public IList<ReportQueryFilterModel> Filters { get; set; } = new List<ReportQueryFilterModel>();
}

public class GetReportBuilderByIdQueryHandler : IRequestHandler<GetReportBuilderByIdQuery, Option<ReportResultModel>>
{
    private readonly ApplicationContext _context;
    public GetReportBuilderByIdQueryHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Option<ReportResultModel>> Handle(GetReportBuilderByIdQuery request, CancellationToken cancellationToken = default)
    {
        var report = await _context.Report
            .Include(l => l.ReportTableList)
            .Include(l => l.ReportTableJoinParameterList)
            .Include(l => l.ReportColumnHeaderList)
            .Include(l => l.ReportFilterGroupingList)
            .Include(l => l.ReportQueryFilterList)
            .Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        if (request.Filters == null || request.Filters.Count == 0)
        {
            request.Filters = new List<ReportQueryFilterModel>();
            if (report?.ReportQueryFilterList?.Count > 0)
            {
                foreach (var parameter in report.ReportQueryFilterList)
                {
                    request.Filters.Add(new ReportQueryFilterModel() { FieldName = parameter.FieldName! });
                }
            }
        }
        var resultsAndLabels = await Helpers.ReportDataHelper.ConvertSQLQueryToJsonAsync(
            _context.Database.GetConnectionString()!,
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
