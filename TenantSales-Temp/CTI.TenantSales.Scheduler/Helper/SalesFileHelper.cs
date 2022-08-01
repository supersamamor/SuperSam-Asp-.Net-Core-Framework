using Microsoft.Extensions.Logging;

namespace CTI.TenantSales.Scheduler.Helper
{
    public class SalesFileHelper
    {
        private readonly ILogger<SalesFileHelper> _logger;
        public SalesFileHelper(ILogger<SalesFileHelper> logger)
        {
            _logger = logger;
        }
        private static void CreateIfNotExists(string FolderPath)
        {
            bool exists = System.IO.Directory.Exists(FolderPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(FolderPath);
        }

        public void MoveSalesFile(string fileDirectory, string destination, string fileName)
        {
            CreateIfNotExists(destination);
            try
            {
                System.IO.File.Move(fileDirectory, destination + @"\" + fileName);
            }
            catch
            {
                try
                {
                    string timeStampString = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
                    CreateIfNotExists(destination + @"\Duplicate\" + timeStampString);
                    System.IO.File.Move(fileDirectory, destination + @"\Duplicate\" + timeStampString + @"\" + fileName);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception : {Message}", ex.Message);
                }
            }
        }

        public static FileInfo[] GetSalesFile(string subdirectory)
        {
            DirectoryInfo d = new(subdirectory);

            FileInfo[] Files = d.GetFiles("*.*");

            return Files;
        }
    }
}

