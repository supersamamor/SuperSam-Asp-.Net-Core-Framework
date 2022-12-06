using CTI.FAS.Core.Identity;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using CTI.Common.Services.Shared.Interfaces;
using CTI.Common.Services.Shared.Models.Mail;
using CTI.FAS.Core.Constants;

namespace CTI.FAS.Scheduler.Jobs
{
    public class EnrollmentNotificationJob : IJob
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EnrollmentNotificationJob> _logger;
        private readonly string _baseUrl;
        private readonly IMailService _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        public EnrollmentNotificationJob(ApplicationContext context, ILogger<EnrollmentNotificationJob> logger, IConfiguration configuration, IMailService emailSender,
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
            await ProcessEnrollmentNotificationAsync();
            await ProcessPaymentTransactionNotificationAsync();
        }
        private async Task ProcessEnrollmentNotificationAsync()
        {
            var enrollmentBatchList = await _context.EnrollmentBatch.Where(l => l.EmailStatus == EmailStatus.Pending)
                .IgnoreQueryFilters().ToListAsync();
            foreach (var item in enrollmentBatchList)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(item.UserId);
                    await SendEnrollmentNotification(item, user);
                    item.TagAsSent();
                }
                catch (Exception ex)
                {
                    item.TagAsFailed();
                    _logger.LogError(ex, @"ProcessEnrollmentNotificationAsync Error Message : {Message} / StackTrace : {StackTrace}", ex.Message, ex.StackTrace);
                }
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
        }
        private async Task SendEnrollmentNotification(EnrollmentBatchState enrollmentBatch, ApplicationUser user)
        {
            string subject = "Creditor Account Enrollment Notification";
            string message = GenerateEnrollmentEmailTemplate(user);
            await _emailSender.SendAsync(new MailRequest()
            {
                To = user.Email,
                Subject = subject,
                Body = message,
                Attachments = new List<string>() { enrollmentBatch.FilePath! }
            });
        }
        private static string GenerateEnrollmentEmailTemplate(ApplicationUser user)
        {
            string str = "<span style='font-size:10pt; font-weight:bold; font-family:Arial;'> ";
            str += "Creditor Account Enrollment Notification";
            str += "<br />";
            str += "</span>";
            str += "<br />";            
            str += "<span style+='font-size:10pt; font-family:Arial;'> ";
            str += $"Hi {user.Name},";
            str += "<br />";
            str += "<br />";
            str += "See Attached for the list of creditor account for approval.";
            str += "<br />";
            str += "<br />";
            str += "<i>This is a system-generated email. Please do not reply.<i/>";
            return str;
        }

        private async Task ProcessPaymentTransactionNotificationAsync()
        {
            var paymentTransactionBatchList = await _context.Batch.Where(l => l.EmailStatus == EmailStatus.Pending)
                .IgnoreQueryFilters().ToListAsync();
            foreach (var item in paymentTransactionBatchList)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(item.UserId);
                    await SendPaymentTransactionNotification(item, user);
                    item.TagAsSent();
                }
                catch (Exception ex)
                {
                    item.TagAsFailed();
                    _logger.LogError(ex, @"ProcessPaymentTransactionNotificationAsync Error Message : {Message} / StackTrace : {StackTrace}", ex.Message, ex.StackTrace);
                }
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
        }
        private async Task SendPaymentTransactionNotification(BatchState PaymentTransactionBatch, ApplicationUser user)
        {
            string subject = "E-Settle Generation Notification";
            string message = GeneratePaymentTransactionEmailTemplate(user);
            await _emailSender.SendAsync(new MailRequest()
            {
                To = user.Email,
                Subject = subject,
                Body = message,
                Attachments = new List<string>() { PaymentTransactionBatch.FilePath! }
            });
        }
        private static string GeneratePaymentTransactionEmailTemplate(ApplicationUser user)
        {
            string str = "<span style='font-size:10pt; font-weight:bold; font-family:Arial;'> ";
            str += "E-Settle Generation Notification";
            str += "<br />";
            str += "</span>";
            str += "<br />";
            str += "<span style+='font-size:10pt; font-family:Arial;'> ";
            str += $"Hi {user.Name},";
            str += "<br />";
            str += "<br />";
            str += "See Attached for the list of payment transactions for approval to bank.";
            str += "<br />";
            str += "<br />";
            str += "<i>This is a system-generated email. Please do not reply.<i/>";
            return str;
        }
    }
}
