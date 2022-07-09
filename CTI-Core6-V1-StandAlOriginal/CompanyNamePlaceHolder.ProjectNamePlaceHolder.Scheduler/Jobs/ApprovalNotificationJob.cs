using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Identity;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Scheduler.Jobs
{
    public class ApprovalNotificationJob : IJob
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ApprovalNotificationJob> _logger;
        private readonly string _baseUrl;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        public ApprovalNotificationJob(ApplicationContext context, ILogger<ApprovalNotificationJob> logger, IConfiguration configuration, IEmailSender emailSender,
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
                                await SendApprovalNotification(item.DataId, user.Email);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, @"ProcessEmailNotificationAsync Error Message : {Message} / StackTrace : {StackTrace}", ex.Message, ex.StackTrace);
                            approvalItem.SendingFailed(ex.Message);
                        }
                    }
                    if (item.ApprovalList.Where(l => l.Status == ApprovalStatus.Approved).Count() == item.ApprovalList.Count)
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
                await _context.SaveChangesAsync();
            }
        }
        private async Task SendApprovalNotification(string dataId, string approverEmail)
        {
            string subject = $"Approval";
            string message = "";
            message += $"Access the ff. link for approval :";
            message += $"<a href='{_baseUrl}/ProjectNamePlaceHolder/MainModulePlaceHolder/Approve?Id={dataId}'>Click this link</a>";
            await _emailSender.SendEmailAsync(approverEmail, subject, message);
        }
    }
}
