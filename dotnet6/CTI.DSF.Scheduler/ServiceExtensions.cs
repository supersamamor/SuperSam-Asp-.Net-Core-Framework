using CTI.DSF.Scheduler.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.AspNetCore;
using Quartz.Job;
namespace CTI.DSF.Scheduler
{
    public static class ServiceExtensions
    {
        public static void AddScheduler(this IServiceCollection services, IConfiguration config)
        {
			if (config.GetValue<bool>("EnableQuartzJob"))
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
				services.AddTransient<BatchUploadJob>();
            }            
        }
    }
}
