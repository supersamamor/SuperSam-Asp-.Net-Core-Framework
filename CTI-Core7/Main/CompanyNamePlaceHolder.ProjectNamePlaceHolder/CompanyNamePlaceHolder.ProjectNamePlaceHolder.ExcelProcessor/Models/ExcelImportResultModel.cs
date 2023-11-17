namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.ExcelProcessor.Models
{
    public class ExcelImportResultModel<TableObject>
    {
        public bool IsSuccess { get; set; }
        public List<ExcelRecord> FailedRecords { get; set; } = new List<ExcelRecord>();
        public List<TableObject> SuccessRecords { get; set; } = new List<TableObject>();
    }
    public class ExcelRecord
    {
        public int RowNumber { get; set; }
        public Dictionary<string, object?> Data { get; set; } = new Dictionary<string, object?>();
    }
}
