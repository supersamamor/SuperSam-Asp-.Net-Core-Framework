using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Helper;
using CTI.DSF.ExcelProcessor.Models;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using System.Reflection;
using CTI.DSF.Infrastructure.Data;

namespace CTI.DSF.ExcelProcessor.Services
{
    public class ExcelService
    {
        public readonly ApplicationContext _context;
        public ExcelService(ApplicationContext context)
        {
            _context = context;
        }
        public static string ExportTemplate<TableObject>(string fullFilePath)
        {
            if (!Directory.Exists(fullFilePath))
                Directory.CreateDirectory(fullFilePath);
            IList<string> restrictedFieldNames = GetRestrictedFieldNames();
            PropertyInfo[] tableObjectFields = GetTableObjectFields<TableObject>();
            string input = typeof(TableObject).Name;
            string wordToRemove = "State";
            var fileName = $"{(input.EndsWith(wordToRemove) ? input[..^wordToRemove.Length] : input)}-BatchUploadTemplate" + ".xlsx";
            var completeFilePath = Path.Combine(fullFilePath, fileName);

            // Check if the file already exists, and delete it if it does
            if (File.Exists(completeFilePath))
                File.Delete(completeFilePath);

            using (var package = new ExcelPackage(new FileInfo(completeFilePath)))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                #region Header and Content
                var columnIndex = 1;
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                foreach (var item in tableObjectFields)
                {
                    if (!restrictedFieldNames.Contains(item.Name, comparer))
                    {
                        workSheet.Cells[1, columnIndex].Value = item.Name;

                        // Check if the property type is nullable (e.g., DateTime?)
                        Type? underlyingType = Nullable.GetUnderlyingType(item.PropertyType);
                        if (underlyingType != null)
                        {
                            switch (underlyingType)
                            {
                                case Type dateTimeType when dateTimeType == typeof(DateTime):
                                    workSheet.Column(columnIndex).Style.Numberformat.Format = "mm/dd/yyyy";
                                    workSheet.Cells[2, columnIndex].Value = DateTime.Now;
                                    break;
                                case Type int32Type when int32Type == typeof(int):
                                    workSheet.Column(columnIndex).Style.Numberformat.Format = "#,##0";
                                    workSheet.Cells[2, columnIndex].Value = 0;
                                    break;
                                case Type decimalType when decimalType == typeof(decimal):
                                    workSheet.Column(columnIndex).Style.Numberformat.Format = "#,##0.00";
                                    workSheet.Cells[2, columnIndex].Value = 0;
                                    break;
                                case Type boolType when boolType == typeof(bool):
                                    workSheet.Cells[2, columnIndex].Value = "false";
                                    break;
                                default:
                                    // Handle other data types if needed
                                    workSheet.Cells[2, columnIndex].Value = "Value";
                                    break;
                            }
                        }
                        else
                        {
                            switch (Type.GetTypeCode(item.PropertyType))
                            {
                                case TypeCode.DateTime:
                                    workSheet.Column(columnIndex).Style.Numberformat.Format = "mm/dd/yyyy";
                                    workSheet.Cells[2, columnIndex].Value = DateTime.Now;
                                    break;
                                case TypeCode.Int32:
                                    workSheet.Column(columnIndex).Style.Numberformat.Format = "#,##0";
                                    workSheet.Cells[2, columnIndex].Value = 0;
                                    break;
                                case TypeCode.Decimal:
                                    workSheet.Column(columnIndex).Style.Numberformat.Format = "#,##0.00";
                                    workSheet.Cells[2, columnIndex].Value = 0;
                                    break;
                                case TypeCode.Boolean:
                                    workSheet.Cells[2, columnIndex].Value = "false";
                                    break;
                                default:
                                    workSheet.Cells[2, columnIndex].Value = "Value";
                                    break;
                            }
                        }
                        columnIndex++;
                    }
                }
                string headerRange = "A1:" + NumberHelper.NumberToExcelColumn(columnIndex - 1) + "1";
                workSheet.Cells[headerRange].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[headerRange].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[headerRange].Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                workSheet.Cells[headerRange].Style.Font.Bold = true;
                workSheet.Cells[headerRange].Style.Font.Color.SetColor(Color.White);
                #endregion
                #region Border to All Cells           
                string sheet1modelRange = "A1:" + NumberHelper.NumberToExcelColumn(columnIndex - 1) + "2";
                var sheet1modelTable = workSheet.Cells[sheet1modelRange];
                // Assign borders
                sheet1modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet1modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet1modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet1modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                #endregion
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                package.Save();
            }
            return fileName;
        }
        public async Task<dynamic> ImportAsync<TableObject>(string fullFilePath, CancellationToken token = new CancellationToken()) where TableObject : new()
        {
            IList<TableObject> successfulRecords = new List<TableObject>();
            IList<FailedRecordModel> failedRecords = new List<FailedRecordModel>();
            using (var stream = new MemoryStream())
            {
                byte[] fileBytes = await File.ReadAllBytesAsync(fullFilePath, cancellationToken: token);
                await stream.WriteAsync(fileBytes, cancellationToken: token);
                stream.Position = 0;
                using var package = new ExcelPackage(stream);
                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                var rowCount = workSheet.Dimension.Rows;
                IList<string> restrictedFieldNames = GetRestrictedFieldNames();
                PropertyInfo[] tableObjectFields = GetTableObjectFields<TableObject>();
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                bool isSuccess = true;
                for (int row = 2; row <= rowCount; row++)
                {
                    var columnIndex = 1;
                    var rowValue = new Dictionary<string, object?>(); // Create a new instance for each row                  
                    string errorRemarks = "";
                    foreach (var item in tableObjectFields)
                    {
                        try
                        {
                            if (!restrictedFieldNames.Contains(item.Name, comparer))
                            {
                                // You can't directly set the property value like this; use reflection
                                Type? underlyingType = Nullable.GetUnderlyingType(item.PropertyType);
                                if (underlyingType != null) //Nullable
                                {
                                    var cellValue = workSheet?.Cells[row, columnIndex]?.Value == null ? "" : workSheet!.Cells[row, columnIndex]!.Value.ToString();
                                    if (string.IsNullOrEmpty(cellValue))
                                    {
                                        rowValue.Add(item.Name, cellValue);
                                    }
                                    else
                                    {
                                        switch (underlyingType)
                                        {
                                            case Type dateTimeType when dateTimeType == typeof(DateTime):
                                                rowValue.Add(item.Name, Convert.ToDateTime(cellValue));
                                                break;
                                            case Type int32Type when int32Type == typeof(int):
                                                rowValue.Add(item.Name, Convert.ToInt16(cellValue));
                                                break;
                                            case Type decimalType when decimalType == typeof(decimal):
                                                rowValue.Add(item.Name, Convert.ToDecimal(cellValue));
                                                break;
                                            case Type booleanType when booleanType == typeof(bool):
                                                rowValue.Add(item.Name, Convert.ToBoolean(cellValue));
                                                break;
                                            default:
                                                rowValue.Add(item.Name, Convert.ToString(cellValue));
                                                break;
                                        }
                                    }
                                }
                                else //Not Nullable
                                {
                                    if (workSheet?.Cells[row, columnIndex].Value == null)
                                    {
                                        throw new ArgumentException("Null is not allowed.");
                                    }
                                    switch (Type.GetTypeCode(item.PropertyType))
                                    {
                                        case TypeCode.DateTime:
                                            rowValue.Add(item.Name, Convert.ToDateTime(workSheet.Cells[row, columnIndex].Value));
                                            break;
                                        case TypeCode.Int32:
                                            rowValue.Add(item.Name, Convert.ToInt16(workSheet.Cells[row, columnIndex].Value));
                                            break;
                                        case TypeCode.Decimal:
                                            rowValue.Add(item.Name, Convert.ToDecimal(workSheet.Cells[row, columnIndex].Value));
                                            break;
                                        case TypeCode.Boolean:
                                            rowValue.Add(item.Name, Convert.ToBoolean(workSheet.Cells[row, columnIndex].Value));
                                            break;
                                        default:
                                            rowValue.Add(item.Name, Convert.ToString(workSheet.Cells[row, columnIndex].Value));
                                            break;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            rowValue.Add(item.Name, workSheet?.Cells[row, columnIndex].Value);
                            isSuccess = false;
                            errorRemarks += $"Field `{item.Name}` - " + ex.Message + ";";
                        }
                        columnIndex++;
                    }
                    rowValue = await CustomValidationHandler(typeof(TableObject).Name, rowValue);
                    if (isSuccess)
                    {
                        var columnIndexForSuccessRecord = 1;
                        TableObject successRecord = new();
                        foreach (var item in tableObjectFields)
                        {
                            if (!restrictedFieldNames.Contains(item.Name, comparer))
                            {
                                item.SetValue(successRecord, rowValue[item.Name]);
                            }
                            columnIndexForSuccessRecord++;
                        }
                        successfulRecords.Add(successRecord);
                    }
                    else
                    {
                        failedRecords.Add(new FailedRecordModel() { Data = rowValue, RowNumber = row, Remarks = errorRemarks[0..^2] });
                    }
                }
            }
            if (failedRecords.Count > 0)
            {
                return failedRecords;
            }
            else
            {
                return successfulRecords;
            }
        }
        public string ExportExcelValidationResult<TableObject>(IList<FailedRecordModel> list, string fullFilePath)
        {
            if (!Directory.Exists(fullFilePath))
                Directory.CreateDirectory(fullFilePath);
            PropertyInfo[] tableObjectFields = GetTableObjectFields<TableObject>();
            string input = typeof(TableObject).Name;
            string wordToRemove = "State";
            var fileName = $"{(input.EndsWith(wordToRemove) ? input[..^wordToRemove.Length] : input)}-ValidationResult-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xlsx";
            var completeFilePath = Path.Combine(fullFilePath, fileName);
            IList<string> restrictedFieldNames = GetRestrictedFieldNames();
            using (var package = new ExcelPackage(new FileInfo(completeFilePath)))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                #region Header and Content
                var columnIndex = 1;
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                foreach (var item in tableObjectFields)
                {
                    if (!restrictedFieldNames.Contains(item.Name, comparer))
                    {
                        workSheet.Cells[1, columnIndex].Value = item.Name;
                        // Check if the property type is nullable (e.g., DateTime?)
                        Type? underlyingType = Nullable.GetUnderlyingType(item.PropertyType);
                        if (underlyingType != null)
                        {
                            switch (underlyingType)
                            {
                                case Type dateTimeType when dateTimeType == typeof(DateTime):
                                    workSheet.Column(columnIndex).Style.Numberformat.Format = "mm/dd/yyyy";
                                    break;
                                case Type int32Type when int32Type == typeof(int):
                                    workSheet.Column(columnIndex).Style.Numberformat.Format = "#,##0";
                                    break;
                                case Type decimalType when decimalType == typeof(decimal):
                                    workSheet.Column(columnIndex).Style.Numberformat.Format = "#,##0.00";
                                    break;
                                case Type boolType when boolType == typeof(bool):
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            switch (Type.GetTypeCode(item.PropertyType))
                            {
                                case TypeCode.DateTime:
                                    workSheet.Column(columnIndex).Style.Numberformat.Format = "mm/dd/yyyy";
                                    break;
                                case TypeCode.Int32:
                                    workSheet.Column(columnIndex).Style.Numberformat.Format = "#,##0";
                                    break;
                                case TypeCode.Decimal:
                                    workSheet.Column(columnIndex).Style.Numberformat.Format = "#,##0.00";
                                    break;
                                case TypeCode.Boolean:
                                    break;
                                default:
                                    break;
                            }
                        }
                        columnIndex++;
                    }
                }
                workSheet.Cells[1, columnIndex].Value = "Error Remarks";
                workSheet.Column(columnIndex).Style.Font.Color.SetColor(Color.Red);
                int row = 2;
                foreach (var record in list.OrderBy(l => l.RowNumber))
                {
                    columnIndex = 1;
                    foreach (var item in tableObjectFields)
                    {
                        if (!restrictedFieldNames.Contains(item.Name, comparer))
                        {
                            workSheet.Cells[row, columnIndex].Value = record.Data[item.Name];
                            columnIndex++;
                        }
                    }
                    workSheet.Cells[row, columnIndex].Value = record.Remarks;
                    row++;
                }
                string headerRange = "A1:" + NumberHelper.NumberToExcelColumn(columnIndex) + "1";
                workSheet.Cells[headerRange].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[headerRange].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[headerRange].Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                workSheet.Cells[headerRange].Style.Font.Bold = true;
                workSheet.Cells[headerRange].Style.Font.Color.SetColor(Color.White);
                #endregion
                #region Border to All Cells           
                string sheet1modelRange = "A1:" + NumberHelper.NumberToExcelColumn(columnIndex) + (row - 1);
                var sheet1modelTable = workSheet.Cells[sheet1modelRange];
                // Assign borders
                sheet1modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet1modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet1modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet1modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                #endregion
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                package.Save();
            }
            return completeFilePath;
        }
        private static IList<string> GetRestrictedFieldNames()
        {
            List<string> restrictedFieldNames = new();
            Type baseEntityType = typeof(Common.Core.Base.Models.BaseEntity);
            // Get all the fields of the BaseEntity class          
            PropertyInfo[] baseEntityFields = baseEntityType.GetProperties();
            // Extract and store the field names
            foreach (PropertyInfo field in baseEntityFields)
            {
                restrictedFieldNames.Add(field.Name);
            }
            return restrictedFieldNames;
        }
        private static PropertyInfo[] GetTableObjectFields<TableObject>()
        {
            Type tableObjectType = typeof(TableObject);
            PropertyInfo[] properties = tableObjectType.GetProperties();

            // Include only properties with primitive data types
            properties = properties.Where(prop => prop.PropertyType.IsPrimitive || prop.PropertyType.IsEnum || prop.PropertyType == typeof(string) || prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(DateTime)).ToArray();

            return properties;
        }

        private async Task<Dictionary<string, object?>> CustomValidationHandler(string module, Dictionary<string, object?> rowValue)
        {
            //Implement Custom Validation Here Depending on Model/Table Name
            switch (module)
            {
                case nameof(CompanyState):
                    return rowValue;
                case nameof(DepartmentState):
                    return rowValue;
                case nameof(SectionState):
                    return rowValue;
                case nameof(TeamState):
                    return rowValue;
                case nameof(HolidayState):
                    return rowValue;
                case nameof(TagsState):
                    return rowValue;
                case nameof(TaskMasterState):
                    return rowValue;
                case nameof(TaskCompanyAssignmentState):
                    return rowValue;
                case nameof(TaskApproverState):
                    return rowValue;
                case nameof(TaskTagState):
                    return rowValue;
                case nameof(AssignmentState):
                    return rowValue;
                case nameof(DeliveryState):
                    return rowValue;
                case nameof(DeliveryApprovalHistoryState):
                    return rowValue;

                default: break;
            }
            return rowValue;
        }
    }
}
