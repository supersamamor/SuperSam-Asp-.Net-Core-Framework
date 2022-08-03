using Rotativa.AspNetCore;
namespace CTI.TenantSales.PdfGenerator
{
    public static class RotativaConfigurationSetup
    {
        public static void Setup(string webRootPath)
        {
            RotativaConfiguration.Setup(webRootPath, "Rotativa");
        }
    }
}
