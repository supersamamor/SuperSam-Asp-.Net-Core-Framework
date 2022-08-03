namespace CTI.TenantSales.ExcelProcessor.Models
{
    public class ExportDailySalesReport
    {
        public string FolderName { get; }
        public string Folder { get; }
        public string ExcelName { get; }
        public string DownloadUrl { get; }
        public FileInfo File { get; }

        public ExportDailySalesReport(string folderName, string filename, string extension)
        {
            FolderName = folderName;
            Folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot") + "\\" + FolderName;
            ExcelName = filename + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + extension;
            DownloadUrl = "\\" + FolderName + "\\" + ExcelName;
            File = new FileInfo(Path.Combine(Folder, ExcelName));
        }
    }
}
