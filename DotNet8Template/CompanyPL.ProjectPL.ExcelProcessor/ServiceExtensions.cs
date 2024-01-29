using CompanyPL.ProjectPL.ExcelProcessor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyPL.ProjectPL.ExcelProcessor
{
    public static class ServiceExtensions
    {
        public static void AddExcelProcessor(this IServiceCollection services)
        {
            services.AddTransient<ExcelService>();
        }
    }
}
