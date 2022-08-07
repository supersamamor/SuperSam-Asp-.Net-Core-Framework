using CTI.TenantSales.Infrastructure.Data;
using CTI.TenantSales.Scheduler.Repository.SalesFileProcessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CTI.TenantSales.Scheduler.Jobs
{
    public class RevalidateSalesJob : IJob
    {
        private readonly SalesProcessingRepository _salesProcessingRepository;
        private readonly ApplicationContext _context;
        private readonly ILogger<RevalidateSalesJob> _logger;
        public RevalidateSalesJob(SalesProcessingRepository salesProcessingRepository, ApplicationContext context, ILogger<RevalidateSalesJob> logger)
        {
            _salesProcessingRepository = salesProcessingRepository;
            _context = context;
            _logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var revalidateSales = await _context.Revalidate.Where(l => l.Status == Core.TenantSales.Status.Pending).IgnoreQueryFilters().ToListAsync();
            foreach (var revalidateItem in revalidateSales)
            {
                try
                {
                    await _salesProcessingRepository.TenantSalesValidate();
                    revalidateItem.SetDone();
                }
                catch (Exception ex)
                {
                    revalidateItem.SetFailed(ex.Message);
                    _logger.LogError(ex, "Error in RevalidateSalesJob");
                }
                await _context.SaveChangesAsync(new CancellationToken());
            }
        }
    }
}
