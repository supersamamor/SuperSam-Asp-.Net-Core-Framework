using CTI.TenantSales.Scheduler.Helper;
using CTI.TenantSales.Scheduler.Jobs;
using CTI.TenantSales.Scheduler.Repository.DataSynchronizationRepository;
using CTI.TenantSales.Scheduler.Repository.SalesFileProcessing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Job;
using System.Reflection;

namespace CTI.TenantSales.Scheduler
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<MasterfileSynchronizationRepository>();
            services.AddTransient<ReportDataSynchronizationRepository>();
            services.AddTransient<SalesProcessingRepository>();            
            services.AddTransient<FileScanJob>();
            services.AddTransient<ApprovalNotificationJob>();
            services.AddTransient<SalesFileHelper>();
            services.AddTransient<SalesProcessingJob>();
            services.AddTransient<MasterfileSynchronizationJob>();
            services.AddTransient<ReportDataSynchronizationJob>();
            services.AddTransient<RevalidateSalesJob>();
        }
    }
}
