using CTI.TenantSales.ExcelProcessor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CTI.TenantSales.ExcelProcessor
{
    public static class ServiceExtensions
    {
        public static void AddExcelProcessor(this IServiceCollection services)
        {     
            services.AddTransient<ExportSalesReportSummaryForIFCAFileService>();
        }
    }
}
