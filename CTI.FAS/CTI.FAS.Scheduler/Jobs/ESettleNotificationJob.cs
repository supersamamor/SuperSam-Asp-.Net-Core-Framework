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
            }
        }
        private string GenerateEmailTemplate(PaymentTransactionState paymentTransaction)
        {
            string str = "<span style='font-size:10pt; font-weight:bold; font-family:Arial;'> ";
            str = str + "<b>";
            str = str + paymentTransaction!.EnrolledPayee!.Company!.Name;
            str = str + "</b>";
            str = str + "<br />";
            str = str + "E-Settlement Notification - " + paymentTransaction.DocumentNumber;
            str = str + "<br />";
            str = str + "</span>";
            str = str + "<br />";
            str = str + "<br />";
            str = str + "<span style='font-size:10pt; font-family:Arial;'> ";
            str = str + "Dear Sir/Madam,";
            str = str + "<br />";
            str = str + "<br />";
            str = str + "See Attached for the breakdown of invoices to be paid thru East West Bank E-Settle facility.";
            str = str + "<br />";
            str = str + "<br />";
            str = str + "You will need Adobe Reader in viewing them. Please visit: <a href='http://get.adobe.com/reader/'>http://get.adobe.com/reader/</a> to download the latest version of this software.";
            str = str + "<br />";
            str = str + "Adobe Acrobat Reader - &copy; 2013 Adobe Systems Incorporated. All rights reserved.";
            str = str + "<br />";
            str = str + "<br />";
            str = str + "For inquiries, please call FAI Hotline at 8460278 and ask for the local number of the person copied in this email.";
            str = str + "<br />";
            str = str + "<br />";
            str = str + "<i>This is a system-generated email. Please do not reply.<i/>";
            str = str + "<br />";   
            str = str + "Copyright &copy; 2015 Filinvest Alabang Inc. All rights reserved.";
            str = str + "<br /> ";
            str = str + "<br /> ";
            str = str + "<hr /> ";
            str = str + "<span style='font-family:Times New Roman; font-size:8pt'> ";
            str = str + "<b><p>Email Confidentiality Disclaimer:</b> The information in this electronic message is confidential and/or privileged, and intended for the exclusive use of the ";
            str = str + "addressee. If you are not the intended recipient, you are notified that disclosure, retention, dissemination, copying, alteration and distribution of this ";
            str = str + "communication and/or any attachment, or any part thereof or information therein, are strictly prohibited. If you receive this communication and any ";
            str = str + "attachments in error, kindly notify the sender immediately by email and delete this communication and all attachments. Any views or opinions presented in ";
            str = str + "this email are solely those of the author and do not necessarily represent those of Filinvest Alabang Inc. </p>";
            str = str + "</span> ";
            return str;
        }

    }
}
