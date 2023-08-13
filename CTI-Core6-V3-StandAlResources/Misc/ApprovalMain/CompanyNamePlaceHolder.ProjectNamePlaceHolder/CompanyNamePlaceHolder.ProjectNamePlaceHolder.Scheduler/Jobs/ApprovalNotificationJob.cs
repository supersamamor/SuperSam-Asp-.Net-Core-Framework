using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Identity;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using CompanyNamePlaceHolder.Common.Services.Shared.Interfaces;
using CompanyNamePlaceHolder.Common.Services.Shared.Models.Mail;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Scheduler.Jobs
{
	[DisallowConcurrentExecution]
    public class ApprovalNotificationJob : IJob
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ApprovalNotificationJob> _logger;
        private readonly string _baseUrl;
        private readonly IMailService _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        public ApprovalNotificationJob(ApplicationContext context, ILogger<ApprovalNotificationJob> logger, IConfiguration configuration, IMailService emailSender,
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
            await ProcessEmailNotificationAsync();
        }
        private async Task ProcessEmailNotificationAsync()
        {
            var approvalRecords = await _context.ApprovalRecord.Where(l => l.Status == ApprovalStatus.New || l.Status == ApprovalStatus.PartiallyApproved)
                .Include(l => l.ApprovalList).Include(l => l.ApproverSetup!).IgnoreQueryFilters().ToListAsync();
            foreach (var item in approvalRecords)
            {
                if (item.ApprovalList != null)
                {
                    foreach (var approvalItem in item.ApprovalList)
                    {
                        try
                        {
                            var user = await _userManager.FindByIdAsync(approvalItem.ApproverUserId);
                            if (approvalItem.EmailSendingStatus == SendingStatus.Pending
                                || (item.ApproverSetup!.ApprovalType == ApprovalTypes.InSequence && approvalItem.Sequence == 1 && approvalItem.Status == ApprovalStatus.New))
                            {
                                approvalItem.SendingDone();
                                await SendApprovalNotification(item, user);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, @"ProcessEmailNotificationAsync Error Message : {Message} / StackTrace : {StackTrace}", ex.Message, ex.StackTrace);
                            approvalItem.SendingFailed(ex.Message);
                        }
                    }
                    if (item.ApproverSetup!.ApprovalType == ApprovalTypes.Any && item.ApprovalList.Where(l => l.Status == ApprovalStatus.Approved).Any())
                    {
                        item.Approve();
                    }
                    else if (item.ApprovalList.Where(l => l.Status == ApprovalStatus.Approved || l.Status == ApprovalStatus.Skipped).Count() == item.ApprovalList.Count)
                    {
                        item.Approve();
                    }
                    else if (item.ApprovalList.Where(l => l.Status == ApprovalStatus.Rejected).Any())
                    {
                        item.Reject();
                    }
                    else if (item.ApprovalList.Where(l => l.Status == ApprovalStatus.Approved).Any())
                    {
                        item.PartiallyApprove();
                    }
                }
                _context.Update(item);
                await _context.UpdateRecordFromJobsAsync();
            }
        }
        private async Task SendApprovalNotification(ApprovalRecordState approvalRecord, ApplicationUser user)
        {
            string subject = approvalRecord!.ApproverSetup!.EmailSubject;
            string message = SetApproverName(approvalRecord!.ApproverSetup!.EmailBody, user);
            message = SetApprovalUrl(message, approvalRecord);
            await _emailSender.SendAsync(new MailRequest() { To = user.Email, Subject = subject, Body = message });
        }
        private static string SetApproverName(string message, ApplicationUser user)
        {
            if (message.Contains(EmailContentPlaceHolder.ApproverName))
            {
                message = message.Replace(EmailContentPlaceHolder.ApproverName, user.Name);
            }
            return message;
        }
        private string SetApprovalUrl(string message, ApprovalRecordState approvalRecord)
        {
            if (message.Contains(EmailContentPlaceHolder.ApprovalUrl))
            {
                message = message.Replace(EmailContentPlaceHolder.ApprovalUrl, $"{_baseUrl}/AreaPlaceHolder/{approvalRecord!.ApproverSetup!.TableName}/Approve?Id={approvalRecord.DataId}");
            }
            return message;
        }
        public static class EmailContentPlaceHolder
        {
            public const string ApproverName = "{ApproverName}";
            public const string ApprovalUrl = "{ApprovalUrl}";
        }
    }
}
