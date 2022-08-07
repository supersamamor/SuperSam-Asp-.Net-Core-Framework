using CTI.TenantSales.Scheduler.Repository.SalesFileProcessing;
using Quartz;

namespace CTI.TenantSales.Scheduler.Jobs
{
    public class SalesProcessingJob : IJob
    {
        private readonly SalesProcessingRepository _salesProcessingRepository;

        public SalesProcessingJob(SalesProcessingRepository salesProcessingRepository)
        {
            _salesProcessingRepository = salesProcessingRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _salesProcessingRepository.TenantSalesValidate();
            await _salesProcessingRepository.ProcessSalesFile();
        }
    }
}
