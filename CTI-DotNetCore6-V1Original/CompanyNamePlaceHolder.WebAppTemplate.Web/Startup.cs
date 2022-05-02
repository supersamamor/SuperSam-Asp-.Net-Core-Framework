using AspNetCoreHero.ToastNotification;
using CTI.Common.Services.Shared;
using CTI.Common.Web.Utility.Annotations;
using CTI.Common.Web.Utility.Logging;
using CompanyNamePlaceHolder.WebAppTemplate.Application;
using CompanyNamePlaceHolder.WebAppTemplate.Infrastructure.Data;
using CompanyNamePlaceHolder.WebAppTemplate.Web.Areas.Identity.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Reflection;

namespace CompanyNamePlaceHolder.WebAppTemplate.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationInsightsTelemetry();
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddControllersWithViews();
        services.ConfigureSecurity(Configuration);
        services.ConfigureAuthorization();
        services.ConfigureLocalization();
        services.AddRazorPages().ConfigureLocalization();
        services.AddApplicationServices();
        services.AddSharedServices(Configuration);
        services.AddQuartz();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddWebOptimizer();
        if (Configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseInMemoryDatabase("ApplicationContext"));
        }
        else
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationContext"),
                                     o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));
        }
        services.AddNotyf(config =>
        {
            config.DurationInSeconds = 10;
            config.IsDismissable = true;
            config.Position = NotyfPosition.BottomRight;
        });
        services.AddHealthChecks()
                .AddDbContextCheck<ApplicationContext>()
                .AddDbContextCheck<IdentityContext>();
        services.AddLogEnricherServices();
        services.AddSingleton<IValidationAttributeAdapterProvider, CustomValidationAttributeAdapterProvider>();
    }
}
