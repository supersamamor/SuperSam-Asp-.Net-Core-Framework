using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.ViewAttachment
{
    [Authorize]
    public class IndexModel : BasePageModel<IndexModel>
    {
        private readonly IConfiguration _configuration;
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet(string subFolder, string id, string fieldName, string fileName)
        {
            try
            {
                var uploadFilesPath = _configuration.GetValue<string>("UsersUpload:UploadFilesPath");
                // Construct the path to the requested file in your static folder.
                string filePath = Path.Combine(uploadFilesPath!, subFolder, id, fieldName,
                    fileName!);

                // Serve the file.
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(fileStream, GetContentType(fileName!));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error fetching the attachment. File Name {FileName}", fileName);
                return NotFound();
            }
        }
        private static string GetContentType(string fileName)
        {
            // Define content types based on file extensions.
            if (fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // MIME type for Excel files
            }
            else if (fileName.EndsWith(".pptx", StringComparison.OrdinalIgnoreCase))
            {
                return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            }
            else if (fileName.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
            {
                return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            }
            else if (fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            {
                return "image/png";
            }
            else if (fileName.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
            {
                return "image/gif";
            }
            else if (fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return "image/jpeg";
            }
            else if (fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return "application/pdf";
            }
            else if (fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
            {
                return "application/zip";
            }
            else if (fileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
            {
                return "application/vnd.ms-excel";
            }
            else if (fileName.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
            {
                return "image/svg+xml";
            }
            // Add more content type mappings as needed.
            return "application/octet-stream"; // Default content type for other file types
        }
    }
}
