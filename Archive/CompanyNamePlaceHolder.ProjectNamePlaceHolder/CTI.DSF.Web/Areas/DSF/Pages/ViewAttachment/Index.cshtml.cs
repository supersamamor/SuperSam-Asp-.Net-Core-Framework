using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.ViewAttachment
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
                return File(fileStream, Helper.ContentTypeHelper.GetContentType(fileName!));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error fetching the attachment. File Name {FileName}", fileName);
                return NotFound();
            }
        }
        
    }
}
