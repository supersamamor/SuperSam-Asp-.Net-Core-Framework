using CompanyNamePlaceHolder.ProjectNamePlaceHolder.ExcelProcessor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.ExcelProcessor
{
    public static class ServiceExtensions
    {
        public static void AddExcelProcessor(this IServiceCollection services)
        {
            services.AddTransient<ExcelService>();
        }
    }
}
