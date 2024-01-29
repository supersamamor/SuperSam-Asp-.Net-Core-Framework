namespace CompanyPL.ProjectPL.Web.Models;
public record BatchUploadModel
{  
    public IFormFile? BatchUploadForm { get; set; }
    public string? BatchUploadFileName { get; set; }
}
