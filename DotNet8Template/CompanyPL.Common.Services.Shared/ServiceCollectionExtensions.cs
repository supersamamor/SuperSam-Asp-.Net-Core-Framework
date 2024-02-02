using CompanyPL.Common.Services.Shared.Interfaces;
using CompanyPL.Common.Services.Shared.Services.SmtpMail;
using CompanyPL.Common.Services.Shared.Services.InMemoryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyPL.Common.Services.Shared;

/// <summary>
/// Extension methods for enabling shared services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register the shared services in the DI
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddSharedServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));      
	    services.AddSingleton<InMemoryStorageService>();
    }
}