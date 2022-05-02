using CTI.Common.Web.Utility.Identity;
using CTI.Common.Web.Utility.Logging;
using CompanyNamePlaceHolder.WebAppTemplate.Application;
using CompanyNamePlaceHolder.WebAppTemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CompanyNamePlaceHolder.WebAppTemplate.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.ConfigureAuthentication(Configuration);
        services.ConfigureAuthorization();
        services.ConfigureVersioning();
        services.ConfigureSwagger();
        services.AddApplicationServices();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddApplicationInsightsTelemetry();
        services.AddHealthChecks()
                .AddDbContextCheck<ApplicationContext>();
        if (Configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseInMemoryDatabase("ApplicationContext"));
        }
        else
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationContext")));
        }
        services.AddLogEnricherServices();
        services.AddTransient<IAuthenticatedUser, DefaultAuthenticatedUser>();
    }
}
