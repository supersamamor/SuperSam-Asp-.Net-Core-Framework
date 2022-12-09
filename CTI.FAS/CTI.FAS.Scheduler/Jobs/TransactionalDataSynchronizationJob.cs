using CTI.FAS.Infrastructure.Data;
using CTI.FAS.Scheduler.Repository.DataSynchronizationRepository;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Data;

namespace CTI.FAS.Scheduler.Jobs
{
    [DisallowConcurrentExecution]
    public class TransactionalDataSynchronizationJob : IJob
    {
        private readonly ApplicationContext _context;
        private readonly TransactionalDataSynchronizationRepository _transactionalDataSynchronizationRepository;
        public TransactionalDataSynchronizationJob(ApplicationContext context, TransactionalDataSynchronizationRepository transactionalDataSynchronizationRepository)
        {
            _context = context;
            _transactionalDataSynchronizationRepository = transactionalDataSynchronizationRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await ProcessTransactionalDataSynchronization();
        }
        private async Task ProcessTransactionalDataSynchronization()
        {
            var databaseConnectionSetupList = await _context.DatabaseConnectionSetup.Where(l => l.IsDisabled == false).IgnoreQueryFilters().AsNoTracking().ToListAsync();
            foreach (var databaseConnectionItem in databaseConnectionSetupList)
            {
                await _transactionalDataSynchronizationRepository.RunTransactionalDataSynchronizationScript(databaseConnectionItem);
            }          
        }
    }
}
