using CTI.SQLReportAutoSender.ReportGenerator.Helper;
using Microsoft.Extensions.DependencyInjection;
namespace CTI.SQLReportAutoSender.ReportGenerator
{
    public static class ServiceCollectionExtensions
    {
        public static void AddReportGeneratorService(this IServiceCollection services)
        {
            services.AddTransient<ExcelReportGeneratorService>();
        }
    }
}
