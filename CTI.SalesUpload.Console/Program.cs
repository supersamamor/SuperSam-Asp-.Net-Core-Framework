using CTI.SalesUpload.Console.Constants;
using CTI.SalesUpload.Console.Models;
using CTI.SalesUpload.Console.Services;
using NLog;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;

namespace CTI.SalesUpload.Console
{
    internal class Program
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            var authService = new AuthenticationService(httpClient);
            string apiConnectionType = ApiConnectionType.Public;
            JwToken token;
            try
            {
                token = authService.GetJwTokenAsync(ConfigurationManager.AppSettings["AuthenticationUrl"], new System.Threading.CancellationToken()).Result;
            }
            catch 
            {
                apiConnectionType = ApiConnectionType.Private;
                token = authService.GetJwTokenAsync(ConfigurationManager.AppSettings["AuthenticationUrlPrivate"], new System.Threading.CancellationToken()).Result;
            }
            
            var uploadSalesFileService = new SalesFileUploadService(httpClient);

            bool exists = Directory.Exists(ConfigurationManager.AppSettings["FilePath"]);
            if (!exists)
            {
                Directory.CreateDirectory(ConfigurationManager.AppSettings["FilePath"]);
            }
            var files = GetFiles(ConfigurationManager.AppSettings["FilePath"]);
            var tenantSalesUrl = ConfigurationManager.AppSettings["TenantSalesApiUrl"];
            if (apiConnectionType == ApiConnectionType.Private)
            {
                tenantSalesUrl = ConfigurationManager.AppSettings["TenantSalesApiUrlPrivate"];
            }
            foreach (var file in files)
            {
                try
                {
                    uploadSalesFileService.Upload(tenantSalesUrl, ConfigurationManager.AppSettings["FilePath"] + "\\" + file.Name, token.AccessToken, new System.Threading.CancellationToken());
                    MoveFiles("Success", file.Name);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"An error has occured while uploading the file : {file.Name}");
                    MoveFiles("Failed", file.Name);
                }             
            }
        }
        private static FileInfo[] GetFiles(string directory)
        {
            DirectoryInfo d = new DirectoryInfo(directory);
            FileInfo[] Files = d.GetFiles("*.*");
            return Files;
        }
        private static void MoveFiles(string subFolder, string fileName)
        {
            var destinationFolder = ConfigurationManager.AppSettings["FilePath"] + "\\" + subFolder + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
            bool exists = Directory.Exists(destinationFolder);
            if (!exists)
            {
                Directory.CreateDirectory(destinationFolder);
            }
            File.Move(ConfigurationManager.AppSettings["FilePath"] + "\\" + fileName, destinationFolder + "\\" + fileName);
        }
    }
}
