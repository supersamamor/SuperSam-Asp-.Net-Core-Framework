using CTI.SQLReportAutoSender.Core.Identity;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Globalization;

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
            await ProcessReportSending();
        }
        private async Task ProcessReportToInbox()
        {
            DateTime currentDate = DateTime.Now;
            var reportList = await (from a in _context.Report
                                    .Include(l => l.ScheduleFrequency)
                                    .Include(l => l.ReportScheduleSettingList!).ThenInclude(l => l.ScheduleParameter)
                                    .Include(l => l.CustomScheduleList)
                                    join b in _context.ApprovalRecord on a.Id equals b.DataId
                                    where a.IsActive == true && b.Status == ApprovalStatus.Approved
                                    select a).ToListAsync();
            foreach (var item in reportList)
            {
                int dayNo = 0;
                TimeSpan time = currentDate.TimeOfDay;
                string dayName = "";
                foreach (var schedParam in item?.ReportScheduleSettingList!)
                {
                    if (schedParam.ScheduleParameter!.Description == ScheduleParameter.Dayname)
                    {
                        dayNo = Convert.ToInt32(schedParam.Value);
                    }
                    else if (schedParam.ScheduleParameter!.Description == ScheduleParameter.Daynumber)
                    {
                        DateTime dateTime = DateTime.ParseExact(schedParam.Value,
                                       "h:mm tt", CultureInfo.InvariantCulture);
                        time = dateTime.TimeOfDay;
                    }
                    else if (schedParam.ScheduleParameter!.Description == ScheduleParameter.Time)
                    {
                        dayName = schedParam.Value;
                    }
                }
                if (time >= currentDate.TimeOfDay)
                {
                    bool hasScheduleToday = false;
                    if (item.ScheduleFrequency!.Description == Frequency.Monthly)
                    {
                        if (dayNo == currentDate.Day) { hasScheduleToday = true; }
                    }
                    else if (item.ScheduleFrequency!.Description == Frequency.Weekly)
                    {
                        if (dayName == currentDate.DayOfWeek.ToString()) { hasScheduleToday = true; }
                    }
                    else if (item.ScheduleFrequency!.Description == Frequency.CustomDates)
                    {
                        foreach (var customSchedule in item?.CustomScheduleList!)
                        {
                            time = customSchedule.DateTimeSchedule.TimeOfDay;
                            if (customSchedule.DateTimeSchedule.Date == currentDate.Date) { hasScheduleToday = true; break; }
                        }
                    }
                    if (hasScheduleToday)
                    {
                        var reportDateTime = currentDate.Date.Add(time);
                        if (!await _context.ReportInbox.Where(l => l.ReportDateTime == reportDateTime && l.ReportId == item.Id).AnyAsync())
                        {
                            await _context.AddAsync(new ReportInboxState()
                            {
                                ReportId = item.Id,
                                Status = ReportStatus.Pending,
                                ReportDateTime = reportDateTime,
                            });
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
        }
        private async Task ProcessReportSending()
        {
            var reportInboxList = await (from a in _context.ReportInbox
                                    .Include(l => l.Report).ThenInclude(l => l!.ReportDetailList)
                                    .Include(l => l.Report).ThenInclude(l => l!.MailSettingList)
                                    .Include(l => l.Report).ThenInclude(l => l!.MailRecipientList)
                                         where a.Status == ReportStatus.Pending
                                         select a).ToListAsync();
            foreach (var reportInbox in reportInboxList)
            {
                foreach (var reportDetail in reportInbox.Report!.ReportDetailList!)
                {

                }
            }
        }
    }
}
