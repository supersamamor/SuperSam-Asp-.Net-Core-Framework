using CTI.TenantSales.PdfGenerator.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CTI.TenantSales.PdfGenerator
{
    public static class ServiceExtensions
    {
        public static void AddPdfGenerator(this IServiceCollection services)
        {       
            services.AddTransient<DailySalesReportToPdf>();
        }
    }
}
