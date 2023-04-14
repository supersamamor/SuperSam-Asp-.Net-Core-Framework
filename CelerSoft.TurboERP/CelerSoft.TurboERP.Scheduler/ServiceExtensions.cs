using CelerSoft.TurboERP.Scheduler.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Job;
namespace CelerSoft.TurboERP.Scheduler
{
    public static class ServiceExtensions
    {
        public static void AddScheduler(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<QuartzOptions>(config.GetSection("Quartz"));
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
            });
            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });
            services.AddTransient<FileScanJob>();
            services.AddTransient<ApprovalNotificationJob>();
        }
    }
}
