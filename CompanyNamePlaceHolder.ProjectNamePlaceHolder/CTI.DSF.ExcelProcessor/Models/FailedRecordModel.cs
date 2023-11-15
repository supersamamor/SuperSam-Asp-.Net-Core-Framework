namespace CTI.DSF.ExcelProcessor.Models
{
    public class FailedRecordModel
    {
        public int RowNumber { get; set; }
        public required Dictionary<string, object?> Data { get; set; } 
        public string Remarks { get; set; } = "";
    }
}
