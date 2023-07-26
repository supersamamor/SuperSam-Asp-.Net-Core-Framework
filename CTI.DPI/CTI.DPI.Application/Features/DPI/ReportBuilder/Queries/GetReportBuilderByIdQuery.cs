using CTI.DPI.Application.DTOs;
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
        return new ReportResultModel()
        {
            ReportId = request.Id,
            Result = ConvertSQLQueryToJson(report!, request.Filters),
            ReportOrChartType = report!.ReportOrChartType,
            Filters = request.Filters,
        };
    }
    private string ConvertSQLQueryToJson(ReportState report, IList<ReportQueryFilterModel> filters)
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
        List<Dictionary<string, object>> result = new();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            // Step 2 and 3: Iterate through the SqlDataReader and store rows as dictionaries
            while (reader.Read())
            {
                Dictionary<string, object> row = new();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader[i];
                }
                result.Add(row);
            }
        }
        // Step 4: Serialize the list of dictionaries to JSON
        return JsonConvert.SerializeObject(result, Formatting.Indented);
    }

}
