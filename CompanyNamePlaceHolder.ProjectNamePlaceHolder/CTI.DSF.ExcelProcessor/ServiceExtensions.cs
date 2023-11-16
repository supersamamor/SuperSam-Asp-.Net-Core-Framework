using CTI.DSF.ExcelProcessor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CTI.DSF.ExcelProcessor
{
    public static class ServiceExtensions
    {
        public static void AddExcelProcessor(this IServiceCollection services)
        {
            services.AddTransient<ExcelService>();
        }
    }
}
