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
           
        }
      
    }
}
