using CTI.DPI.Application.DTOs;
using CTI.DPI.Core.Constants;
using CTI.DPI.Core.DPI;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace CTI.DPI.Application.Helpers
{
    public static class ReportDataHelper
    {
        public static async Task<LabelResultAndStyle> ConvertSQLQueryToJsonAsync(string connectionString, ReportState report, IList<ReportQueryFilterModel>? filters = null)
        {
            using SqlConnection connection = new(connectionString);
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
            List<Dictionary<string, object>> tableData = new();
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            int index = 0;
            if (report.ReportOrChartType == ReportChartType.Table)
            {
                while (reader.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (index == 0)
                        {
                            labels.Add(reader.GetName(i));
                        }
                        row[reader.GetName(i)] = reader[i];
                    }
                    tableData.Add(row);
                    index++;
                }
                return new LabelResultAndStyle()
                {
                    Results = JsonConvert.SerializeObject(tableData, Formatting.Indented),
                    Labels = JsonConvert.SerializeObject(labels, Formatting.Indented),
                };
            }
            else
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (string.Equals(reader.GetName(i), "Label", StringComparison.OrdinalIgnoreCase))
                        {
                            labels.Add(reader[i]?.ToString());
                        }
                        else if (string.Equals(reader.GetName(i), "Data", StringComparison.OrdinalIgnoreCase))
                        {
                            results.Add(reader[i]?.ToString());
                        }
                    }
                    colors.Add(Colors.List[index]);
                    index++;
                }
                return new LabelResultAndStyle()
                {
                    Results = JsonConvert.SerializeObject(results, Formatting.Indented),
                    Labels = JsonConvert.SerializeObject(labels, Formatting.Indented),
                    Colors = JsonConvert.SerializeObject(colors, Formatting.Indented)
                };
            }

        }
        public class LabelResultAndStyle
        {
            public string? Results { get; set; }
            public string? Labels { get; set; }
            public string? Colors { get; set; }
        }
    }
}
