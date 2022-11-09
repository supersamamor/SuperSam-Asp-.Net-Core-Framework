using CTI.ELMS.LocationApi.Services;
using CTI.ELMS.LocationApi.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace CTI.ELMS.LocationApi
{
    public static class ServiceCollectionExtensions
    {
        public static void AddLocationApiService(this IServiceCollection services, IConfiguration configuration)
        {       
            services.Configure<LocationApiSettings>(configuration.GetSection("LocationApiSettings"));
            services.AddTransient<LocationApiService>();
            services.AddHttpClient<LocationApiService>(c =>
            {
                c.BaseAddress = new Uri(configuration.GetValue<string>("LocationApiSettings:LocationApiUrl"));
            });
        }
    }
}
