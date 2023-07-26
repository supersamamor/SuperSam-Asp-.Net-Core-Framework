using CTI.DPI.Application.DTOs;
using CTI.DPI.Core.Constants;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using LanguageExt;
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
        var resultsAndLabels = ConvertSQLQueryToJson(report!, request.Filters);
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
    private LabelResultAndStyle ConvertSQLQueryToJson(ReportState report, IList<ReportQueryFilterModel> filters)
    {
        using SqlConnection connection = new(_context.Database.GetConnectionString());
        connection.Open();
        // Step 1: Execute the SELECT query and retrieve the data as a SqlDataReader
        using SqlCommand command = new(report.QueryString, connection);
        if (filters?.Count > 0)
        {
            foreach (var filter in filters)
            {
                command.Parameters.Add($"@{filter.FieldName}", SqlDbType.NVarChar).Value = filter.FieldValue;
            }
        }
        List<string?> results = new();
        List<string?> labels = new();
        List<string?> colors = new();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            // Step 2 and 3: Iterate through the SqlDataReader and store rows as dictionaries
            int index = 0;
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (string.Equals(reader.GetName(i), "Label", StringComparison.OrdinalIgnoreCase))
                    {
                        labels.Add(reader[i]?.ToString());
                    }
                    if (string.Equals(reader.GetName(i), "Data", StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add(reader[i]?.ToString());
                    }
                }
                colors.Add(Colors.List[index]);
                index++;
            }
        }
        // Step 4: Serialize the list of dictionaries to JSON
        return new LabelResultAndStyle()
        {
            Results = JsonConvert.SerializeObject(results, Formatting.Indented),
            Labels = JsonConvert.SerializeObject(labels, Formatting.Indented),
            Colors = JsonConvert.SerializeObject(colors, Formatting.Indented)
        };
    }
    public class LabelResultAndStyle
    {
        public string? Results { get; set; }
        public string? Labels { get; set; }
        public string? Colors { get; set; }
    }
}
