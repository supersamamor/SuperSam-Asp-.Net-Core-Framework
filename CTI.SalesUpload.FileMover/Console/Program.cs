using Console.Services;
using NLog;
using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Console
{
    internal class Program
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            try
            {
                System.Console.WriteLine($"Initializing moving of sales upload file. . .");
                MoveSalesFile();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"An error has occured while moving the salesfile");
            }
            Thread.Sleep(5000);
        }
        private static void MoveSalesFile()
        {
            var projectFolderList = ProjectFolder.GetProjectFolderList(ConfigurationManager.AppSettings["ConnectionString"]);
            foreach (var projectFolder in projectFolderList)
            {
                try
                {
                    var subdirectoryEntries = Directory.GetDirectories(ConfigurationManager.AppSettings["SourceFilePath"] + @"\" + projectFolder);
                    foreach (string subdirectory in subdirectoryEntries)
                    {
                        FileInfo[] Files = GetSalesFile(subdirectory);
                        foreach (FileInfo file in Files)
                        {
                            try
                            {
                                var fileName = file.Name;
                                var fileDirectory = file.DirectoryName + @"\" + fileName;
                                MoveSalesFile(fileDirectory, ConfigurationManager.AppSettings["DestinationFilePath"] + @"\" + projectFolder + @"\"
                                        + subdirectory.Replace(ConfigurationManager.AppSettings["SourceFilePath"] + @"\" + projectFolder, ""), fileName);
                            }
                            catch (Exception ex)
                            {
                                _logger.Error(ex, $"An error has occured while fetching the sales folder of project : {projectFolder} / file : {file.Name}.");
                            }
                        }
                    }
                    System.Console.WriteLine($"Done processing {projectFolder} folder.");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"An error has occured while fetching the sales folder of project : {projectFolder}.");
                }
            }
        }
        public static FileInfo[] GetSalesFile(string subdirectory)
        {
            DirectoryInfo d = new DirectoryInfo(subdirectory);
            FileInfo[] Files = d.GetFiles("*.*");
            return Files;
        }
        private static void MoveSalesFile(string fileDirectory, string destination, string fileName)
        {
            CreateIfNotExists(destination);
            if (!System.IO.File.Exists(destination + @"\" + fileName))
            {
                System.IO.File.Move(fileDirectory, destination + @"\" + fileName);
                System.Console.WriteLine($"Done transferring the sales file to: {destination + @"\" + fileName}");
            }
        }
        private static void CreateIfNotExists(string FolderPath)
        {
            bool exists = System.IO.Directory.Exists(FolderPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(FolderPath);
        }
    }
}
