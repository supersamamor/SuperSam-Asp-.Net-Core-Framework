using CTI.FAS.CsvGenerator.Services;
using Microsoft.Extensions.DependencyInjection;
namespace CTI.FAS.CsvGenerator
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCsvGeneratorService(this IServiceCollection services)
        {
            services.AddTransient<PayeeEnrollmentCsvService>();
            services.AddTransient<PaymentTransactionCsvService>();            
        }
    }
}
