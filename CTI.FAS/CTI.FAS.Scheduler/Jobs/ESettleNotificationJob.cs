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

namespace CTI.FAS.Scheduler.Jobs
{
    public class ESettleNotificationJob : IJob
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ESettleNotificationJob> _logger;
        private readonly string _baseUrl;
        private readonly IMailService _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        public ESettleNotificationJob(ApplicationContext context, ILogger<ESettleNotificationJob> logger, IConfiguration configuration, IMailService emailSender,
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
            await ProcessESettleNotificationAsync();
        }
        private async Task ProcessESettleNotificationAsync()
        {
            var paymentTransactionToEmailList = await _context.PaymentTransaction
                .Include(l => l.EnrolledPayee).ThenInclude(l => l!.Creditor)
                .Include(l => l.EnrolledPayee).ThenInclude(l => l!.Company)
                .IgnoreQueryFilters().Where(l => l.IsForSending == true).ToListAsync();
            foreach (var paymentTransactionForEmailSending in paymentTransactionToEmailList)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(paymentTransactionForEmailSending.ProcessedByUserId);
                    await SendPaymentTransactionNotification(paymentTransactionForEmailSending, user);
                    paymentTransactionForEmailSending.TagAsEmailSent();
                }
                catch (Exception ex)
                {
                    paymentTransactionForEmailSending.TagAsEmailFailed(ex.Message);
                    _logger.LogError(ex, @"ProcessESettleNotificationAsync Error Message : {Message} / StackTrace : {StackTrace}", ex.Message, ex.StackTrace);
                }
                _context.Update(paymentTransactionForEmailSending);
                await _context.SaveChangesAsync();
            }
        }
        private string GenerateEmailTemplate(PaymentTransactionState paymentTransaction)
        {
            string str = "<span style='font-size:10pt; font-weight:bold; font-family:Arial;'> ";
            str += "<b>";
            str += paymentTransaction!.EnrolledPayee!.Company!.Name;
            str += "</b>";
            str += "<br />";
            str += "E-Settlement Notification - " + paymentTransaction.DocumentNumber;
            str += "<br />";
            str += "</span>";
            str += "<br />";
            str += "<br />";
            str += "<span style+='font-size:10pt; font-family:Arial;'> ";
            str += "Dear Sir/Madam,";
            str += "<br />";
            str += "<br />";
            str += "See Attached for the breakdown of invoices to be paid thru East West Bank E-Settle facility.";
            str += "<br />";
            str += "<br />";
            str += "You will need Adobe Reader in viewing them. Please visit: <a href+='http://get.adobe.com/reader/'>http://get.adobe.com/reader/</a> to download the latest version of this software.";
            str += "<br />";
            str += "Adobe Acrobat Reader - &copy; 2013 Adobe Systems Incorporated. All rights reserved.";
            str += "<br />";
            str += "<br />";
            str += "For inquiries, please call FAI Hotline at 8460278 and ask for the local number of the person copied in this email.";
            str += "<br />";
            str += "<br />";
            str += "<i>This is a system-generated email. Please do not reply.<i/>";
            str += "<br />";
            str += "Copyright &copy; 2015 Filinvest Alabang Inc. All rights reserved.";
            str += "<br /> ";
            str += "<br /> ";
            str += "<hr /> ";
            str += "<span style+='font-family:Times New Roman; font-size:8pt'> ";
            str += "<b><p>Email Confidentiality Disclaimer:</b> The information in this electronic message is confidential and/or privileged, and intended for the exclusive use of the ";
            str += "addressee. If you are not the intended recipient, you are notified that disclosure, retention, dissemination, copying, alteration and distribution of this ";
            str += "communication and/or any attachment, or any part thereof or information therein, are strictly prohibited. If you receive this communication and any ";
            str += "attachments in error, kindly notify the sender immediately by email and delete this communication and all attachments. Any views or opinions presented in ";
            str += "this email are solely those of the author and do not necessarily represent those of Filinvest Alabang Inc. </p>";
            str += "</span> ";
            return str;
        }
        private async Task SendPaymentTransactionNotification(PaymentTransactionState paymentTransaction, ApplicationUser? user)
        {
            string subject = $"E-Settlement Notification - {paymentTransaction.DocumentNumber}";
            string message = GenerateEmailTemplate(paymentTransaction);
            var emailRequest = new MailRequest()
            {
                To = paymentTransaction.EnrolledPayee!.Email,
                Subject = subject,
                Body = message,
                Attachments = new List<string>() { paymentTransaction.PdfFilePath! }
            };
            if (user?.Email != null)
            {
                emailRequest.Bcc = new List<string>() { user!.Email };
            }
            if (paymentTransaction?.EnrolledPayee?.EnrolledPayeeEmailList != null)
            {
                emailRequest.Ccs = new List<string>();
                foreach (var ccItem in paymentTransaction!.EnrolledPayee!.EnrolledPayeeEmailList)
                {
                    emailRequest!.Ccs!.Add(ccItem.Email);
                }
            }
            await _emailSender.SendAsync(emailRequest);
        }
    }
}
