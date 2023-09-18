using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.DTOs;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Constants;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
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
            HashSet<string?> columnHeaders = new();
            List<string?> colors = new();
            List<Dictionary<string, object>> tableData = new();
            List<Dictionary<string, string>> tableColumnLabel = new();
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            int colorIndex = 0;
            if (report.ReportOrChartType == ReportChartType.Table)
            {
                while (reader.Read())
                {
                    Dictionary<string, object> rowData = new();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var sanitizedDataName = StringHelper.Sanitize(reader.GetName(i));
                        if (colorIndex == 0)
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
                    colorIndex++;
                }
                return new LabelResultAndStyle()
                {
                    Results = JsonConvert.SerializeObject(tableData, Formatting.Indented),
                    ColumnHeaders = JsonConvert.SerializeObject(tableColumnLabel, Formatting.Indented),
                };
            }
            else
            {
                bool isMultipleResultSet = reader.FieldCount > 2;
                Dataset singleDataSet = new() { };
                HashSet<string?> dataSetTitles = new();
                Dictionary<string, MultipleDataset> multipleDataSetList = new();
                Dictionary<string, Dataset> dataSetList = new();
                int multipleDatasetColorIndex = 0;
                while (reader.Read())
                {
                    Dictionary<string, object> rowData = new();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (string.Equals(reader.GetName(i), "Label", StringComparison.OrdinalIgnoreCase))
                        {
                            columnHeaders.Add(reader[i]?.ToString());
                        }
                        else
                        {
                            if (IsNumeric(reader[i]!.ToString()!))
                            {
                                if (isMultipleResultSet)
                                {
                                    switch (report.ReportOrChartType)
                                    {
                                        case ReportChartType.HorizontalBar:
                                            if (!dataSetTitles.Contains(reader.GetName(i)))
                                            {
                                                dataSetTitles.Add(reader.GetName(i));
                                                var dataSet = new MultipleDataset()
                                                {
                                                    Label = reader.GetName(i),
                                                    Data = { Convert.ToDecimal(reader[i]!.ToString()!) },
                                                    BackgroundColor = Colors.List[multipleDatasetColorIndex],
                                                };
                                                multipleDataSetList[reader.GetName(i)] = dataSet;
                                                multipleDatasetColorIndex++;
                                                if (multipleDatasetColorIndex > Colors.List.Length() - 1)
                                                {
                                                    multipleDatasetColorIndex = 0;
                                                }
                                            }
                                            else
                                            {
                                                var existingDataSet = multipleDataSetList[reader.GetName(i)];
                                                existingDataSet.Data.Add(Convert.ToDecimal(reader[i]!.ToString()!));
                                                multipleDataSetList[reader.GetName(i)] = existingDataSet;
                                            }
                                            break;
                                        case ReportChartType.Pie:
                                            if (!dataSetTitles.Contains(reader.GetName(i)))
                                            {
                                                dataSetTitles.Add(reader.GetName(i));
                                                var dataSet = new Dataset()
                                                {
                                                    Label = reader.GetName(i),
                                                    Data = { Convert.ToDecimal(reader[i]!.ToString()!) },
                                                    BackgroundColor = { Colors.List[colorIndex] },
                                                };
                                                dataSetList[reader.GetName(i)] = dataSet;
                                            }
                                            else
                                            {
                                                var existingDataSet = dataSetList[reader.GetName(i)];
                                                existingDataSet.Data.Add(Convert.ToDecimal(reader[i]!.ToString()!));
                                                existingDataSet.BackgroundColor.Add(Colors.List[colorIndex]);
                                                dataSetList[reader.GetName(i)] = existingDataSet;
                                            }
                                            break;
                                        default: break;
                                    }
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(singleDataSet.Label))
                                    {
                                        singleDataSet.Label = reader.GetName(i);
                                    }
                                    singleDataSet.Data.Add(Convert.ToDecimal(reader[i]!.ToString()!));
                                    singleDataSet.BackgroundColor.Add(Colors.List[colorIndex]);
                                }
                            }
                        }
                    }
                    colorIndex++;
                    if (colorIndex > Colors.List.Length() - 1)
                    {
                        colorIndex = 0;
                    }
                }
                if (isMultipleResultSet)
                {
                    switch (report.ReportOrChartType)
                    {
                        case ReportChartType.HorizontalBar:
                            return new LabelResultAndStyle()
                            {
                                Results = JsonConvert.SerializeObject(new List<MultipleDataset>(multipleDataSetList.Values), Formatting.Indented),
                                ColumnHeaders = JsonConvert.SerializeObject(columnHeaders, Formatting.Indented),
                                DisplayLegend = true,
                            };
                        case ReportChartType.Pie:
                            return new LabelResultAndStyle()
                            {
                                Results = JsonConvert.SerializeObject(new List<Dataset>(dataSetList.Values), Formatting.Indented),
                                ColumnHeaders = JsonConvert.SerializeObject(columnHeaders, Formatting.Indented),
                                DisplayLegend = true,
                            };
                        default:
                            return new LabelResultAndStyle();
                    }
                }
                else
                {
                    return new LabelResultAndStyle()
                    {
                        Results = JsonConvert.SerializeObject(new List<object> { singleDataSet }, Formatting.Indented),
                        ColumnHeaders = JsonConvert.SerializeObject(columnHeaders, Formatting.Indented),
                        Colors = JsonConvert.SerializeObject(colors, Formatting.Indented),
                        DisplayLegend = false,
                    };
                }
            }

        }
        public static async Task<List<Dictionary<string, string?>>> ConvertTableKeyValueToDictionary(string connectionString, string tableKeyValue, string? filter)
        {
            string[] queryComponents = tableKeyValue.Split(',');
            var query = $"select Distinct {queryComponents[1]} as [Key],{queryComponents[2]} as [Value] from {queryComponents[0]}";
            if (!string.IsNullOrEmpty(filter))
            {
                query += $" where {filter}";
            }
            query += $" order by {queryComponents[2]}";
            var error = Core.Helpers.SQLValidatorHelper.Validate(query);
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
            using SqlConnection connection = new(connectionString);
            connection.Open();
            List<Dictionary<string, string?>> tableData = new();
            using SqlCommand command = new(query, connection);
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                Dictionary<string, string?> rowData = new();
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
            public string? ColumnHeaders { get; set; }
            public string? Colors { get; set; }
            public bool DisplayLegend { get; set; }
        }
        private class Dataset
        {
            [JsonProperty("label")]
            public string? Label { get; set; }
            [JsonProperty("data")]
            public List<decimal> Data { get; set; } = new List<decimal>();
            [JsonProperty("backgroundColor")]
            public List<string> BackgroundColor { get; set; } = new List<string>();
            [JsonProperty("borderWidth")]
            public int BorderWidth { get; set; } = 1;
        }
        private class MultipleDataset
        {
            [JsonProperty("label")]
            public string? Label { get; set; }
            [JsonProperty("data")]
            public List<decimal> Data { get; set; } = new List<decimal>();
            [JsonProperty("backgroundColor")]
            public string BackgroundColor { get; set; } = "";
            [JsonProperty("borderWidth")]
            public int BorderWidth { get; set; } = 1;
        }
        private static bool IsNumeric(string input)
        {
            return decimal.TryParse(input, out _);
        }
    }
}
