using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.DTOs;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Constants;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using CompanyNamePlaceHolder.Common.Identity.Abstractions;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Helpers
{
    public static class ReportDataHelper
    {
        public static async Task<LabelResultAndStyle> ConvertSQLQueryToJsonAsync(IAuthenticatedUser authenticatedUser, string connectionString, ReportState report, IList<ReportQueryFilterModel>? filters = null)
        {
            var queryWithShortCode = StringHelper.ReplaceCaseInsensitive(report.QueryString!, ShortCodes.CurrentUserId, $"'{authenticatedUser.UserId}'");
            queryWithShortCode = StringHelper.ReplaceCaseInsensitive(queryWithShortCode, ShortCodes.CurrentDateTime, $"GetDate()");
            var error = Core.Helpers.SQLValidatorHelper.Validate(queryWithShortCode);
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
            using SqlConnection connection = new(connectionString);
            connection.Open();
            // Step 1: Execute the SELECT query and retrieve the data as a SqlDataReader
            using SqlCommand command = new(queryWithShortCode, connection);
            if (filters?.Count > 0)
            {
                foreach (var filter in filters)
                {
                    var dataType = SqlDbType.NVarChar;
                    var defaultValue = "";
                    switch (filter.DataType)
                    {
                        case DataTypes.CustomDropdown:
                        case DataTypes.DropdownFromTable:
                            dataType = SqlDbType.NVarChar;
                            break;
                        case DataTypes.Years:
                        case DataTypes.Months:
                            dataType = SqlDbType.Int;
                            defaultValue = "0";
                            break;
                        case DataTypes.Date:
                            dataType = SqlDbType.DateTime;
                            defaultValue = "1900/1/1";
                            break;
                        default
                            :
                            break;
                    }
                    command.Parameters.Add($"@{filter.FieldName}", dataType).Value = string.IsNullOrEmpty(filter.FieldValue) ? defaultValue : filter.FieldValue;
                }
            }
            List<string?> results = new();
            List<string?> labels = new();
            List<string?> colors = new();
            List<Dictionary<string, object>> tableData = new();
            List<Dictionary<string, string>> tableColumnLabel = new();
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            int index = 0;
            if (report.ReportOrChartType == ReportChartType.Table)
            {
                while (reader.Read())
                {
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var sanitizedDataName = StringHelper.Sanitize(reader.GetName(i));
                        if (index == 0)
                        {
                            Dictionary<string, string> columnLabel = new()
                            {
                                ["title"] = StringHelper.ToProperCase(reader.GetName(i)),
                                ["data"] = sanitizedDataName
                            };
                            tableColumnLabel.Add(columnLabel);
                        }
                        rowData[sanitizedDataName] = reader[i];
                    }
                    tableData.Add(rowData);
                    index++;
                }
                return new LabelResultAndStyle()
                {
                    Results = JsonConvert.SerializeObject(tableData, Formatting.Indented),
                    Labels = JsonConvert.SerializeObject(tableColumnLabel, Formatting.Indented),
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
        public static async Task<List<Dictionary<string, string?>>> ConvertTableKeyValueToDictionary(string connectionString, string tableKeyValue, string? filter)
        {
            string[] queryComponents = tableKeyValue.Split(',');
            var query = $"select {queryComponents[1]} as [Key],{queryComponents[2]} as [Value] from {queryComponents[0]}";
            if (!string.IsNullOrEmpty(filter))
            {
                query += $" where {filter}";
            }
            query += $" order by {queryComponents[2]}";
            using SqlConnection connection = new(connectionString);
            connection.Open();
            List<Dictionary<string, string?>> tableData = new();
            using SqlCommand command = new(query, connection);
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                Dictionary<string, string?> rowData = new Dictionary<string, string?>();
                for (int i = 0; i < reader.FieldCount; i++)
                {                 
                    rowData[reader.GetName(i)] = reader[i]?.ToString();                    
                }
                tableData.Add(rowData);
            }
            return tableData;
        }
        public class LabelResultAndStyle
        {
            public string? Results { get; set; }
            public string? Labels { get; set; }
            public string? Colors { get; set; }
        }
    }
}
