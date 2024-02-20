namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
public record BatchUploadModel
{  
    public IFormFile? BatchUploadForm { get; set; }
    public string? BatchUploadFileName { get; set; }
}
