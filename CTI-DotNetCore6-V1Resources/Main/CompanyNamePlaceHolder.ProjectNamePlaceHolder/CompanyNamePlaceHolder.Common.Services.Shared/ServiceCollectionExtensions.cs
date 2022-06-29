using CompanyNamePlaceHolder.Common.Services.Shared.Interfaces;
using CompanyNamePlaceHolder.Common.Services.Shared.Services.SmtpMail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyNamePlaceHolder.Common.Services.Shared
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSharedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
            services.AddTransient<IMailService, SmtpMailService>();
        }
    }
}