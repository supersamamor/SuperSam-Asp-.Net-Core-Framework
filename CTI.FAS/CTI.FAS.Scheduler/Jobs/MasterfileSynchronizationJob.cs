using CTI.FAS.Infrastructure.Data;
using CTI.FAS.Scheduler.Repository.DataSynchronizationRepository;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Data;

namespace CTI.FAS.Scheduler.Jobs
{
    [DisallowConcurrentExecution]
    public class MasterfileSynchronizationJob : IJob
    {
        private readonly ApplicationContext _context;
        private readonly MasterfileSynchronizationRepository _masterfileSynchronizationRepository;
        public MasterfileSynchronizationJob(ApplicationContext context, MasterfileSynchronizationRepository masterfileSynchronizationRepository)
        {
            _context = context;
            _masterfileSynchronizationRepository = masterfileSynchronizationRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await ProcessMasterFileSynchronization();
        }
        private async Task ProcessMasterFileSynchronization()
        {
            var databaseConnectionSetupList = await _context.DatabaseConnectionSetup.Where(l => l.IsDisabled == false).IgnoreQueryFilters().AsNoTracking().ToListAsync();
            foreach (var databaseConnectionItem in databaseConnectionSetupList)
            {
                await _masterfileSynchronizationRepository.RunMasterFileSynchronizationScript(databaseConnectionItem);
            }          
        }
    }
}
