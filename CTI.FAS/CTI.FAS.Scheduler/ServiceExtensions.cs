using CTI.FAS.Scheduler.Jobs;
using CTI.FAS.Scheduler.Repository.DataSynchronizationRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Job;
namespace CTI.FAS.Scheduler
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
            services.AddTransient<MasterfileSynchronizationRepository>();
            services.AddTransient<MasterfileSynchronizationJob>();
            services.AddTransient<TransactionalDataSynchronizationRepository>();
            services.AddTransient<TransactionalDataSynchronizationJob>();
            services.AddTransient<ESettleNotificationJob>();
            services.AddTransient<EnrollmentNotificationJob>();
        }
    }
}
