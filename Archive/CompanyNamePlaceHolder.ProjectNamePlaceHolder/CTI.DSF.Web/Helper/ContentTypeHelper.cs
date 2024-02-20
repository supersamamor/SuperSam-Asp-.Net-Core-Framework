namespace CTI.DSF.Web.Helper
{
    public static class ContentTypeHelper
    {
        public static string GetContentType(string fileName)
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
