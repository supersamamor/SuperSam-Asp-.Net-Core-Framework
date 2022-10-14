using CTI.SQLReportAutoSender.Core.Identity;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CTI.SQLReportAutoSender.Scheduler.Jobs
{
    public class ReportSendingJob : IJob
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ReportSendingJob> _logger;
        private readonly string _baseUrl;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        public ReportSendingJob(ApplicationContext context, ILogger<ReportSendingJob> logger, IConfiguration configuration, IEmailSender emailSender,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _baseUrl = configuration.GetValue<string>("BaseUrl");
            _emailSender = emailSender;
            _userManager = userManager;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await ProcessReportToInbox();
        }
        private async Task ProcessReportToInbox()
        {
            var reportList = await _context.Report.Where(l => l.IsActive == true)
                .Include(l => l.ReportScheduleSettingList).IgnoreQueryFilters().AsNoTracking().ToListAsync();
            foreach (var item in reportList)
            { 
            }
        }       
    }
}
