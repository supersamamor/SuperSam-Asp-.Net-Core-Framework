using CTI.SQLReportAutoSender.Core.Identity;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.EmailSending;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using CTI.SQLReportAutoSender.ReportGenerator.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using System.Globalization;
using System.Net.Mail;

namespace CTI.SQLReportAutoSender.Scheduler.Jobs
{
    public class ReportSendingJob : IJob
    {
        private readonly ApplicationContext _context;
        private readonly ExcelReportGeneratorService _excelReportGeneratorService;
        private readonly MailSettings _mailSettings;
        private readonly ILogger<ReportSendingJob> _logger;
        public ReportSendingJob(ApplicationContext context,
             ExcelReportGeneratorService excelReportGeneratorService, IOptions<MailSettings> mailSettings, ILogger<ReportSendingJob> logger)
        {
            _context = context;
            _excelReportGeneratorService = excelReportGeneratorService;
            _mailSettings = mailSettings.Value;
            _logger = logger;
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
                                    select a).IgnoreQueryFilters().ToListAsync();
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
                                         select a).IgnoreQueryFilters().ToListAsync();
            foreach (var reportInbox in reportInboxList)
            {
                try
                {
                    var listOfReportFiles = _excelReportGeneratorService.GenerateReport(reportInbox);
                    await SendReport(reportInbox, listOfReportFiles);
                    reportInbox.TagAsSent();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, @"ProcessReportSending Error Message : {Message} / StackTrace : {StackTrace}", ex.Message, ex.StackTrace);
                    reportInbox.TagAsFailed(ex.Message);
                }
                await _context.SaveChangesAsync();
            }
        }
        private async Task SendReport(ReportInboxState reportInbox, IList<string> listOfFiles)
        {
            MailMessage message = new();
            foreach (var recipient in reportInbox?.Report?.MailRecipientList!)
            {
                message.To.Add(recipient.RecipientEmail);
            }
            foreach (var file in listOfFiles)
            {
                var attachment = new Attachment(file);
                message.Attachments.Add(attachment);
            }
            message.Subject = reportInbox.Report.MailSettingList!.FirstOrDefault()?.Subject;
            message.Body = reportInbox.Report.MailSettingList!.FirstOrDefault()?.Body;
            using var client = new SmtpClient();
            client.Host = _mailSettings.SMTPHost!;
            client.Port = _mailSettings.SMTPPort;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            if (string.IsNullOrEmpty(reportInbox.Report.MailSettingList?.FirstOrDefault()?.Account))
            {
                message.From = new MailAddress(_mailSettings.SMTPEmail!);
                client.Credentials = new System.Net.NetworkCredential(_mailSettings.SMTPEmail!, _mailSettings.SMTPEmailPassword);
            }
            else
            {
                message.From = new MailAddress(reportInbox.Report.MailSettingList!.FirstOrDefault()!.Account!);
                client.Credentials = new System.Net.NetworkCredential(reportInbox.Report.MailSettingList?.FirstOrDefault()?.Account,
                    reportInbox.Report.MailSettingList?.FirstOrDefault()?.Password);
            }
            await client.SendMailAsync(message);
        }
    }
}
