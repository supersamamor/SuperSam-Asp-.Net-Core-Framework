using CTI.TenantSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTI.TenantSales.Scheduler.Repository.DataSynchronizationRepository
{
    public class MasterfileSynchronizationRepository
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<MasterfileSynchronizationRepository> _logger;
        public MasterfileSynchronizationRepository(ApplicationContext context, ILogger<MasterfileSynchronizationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task RunMasterFileSynchronizationScript(string databaseConnectionSetupId)
        {
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 600;
                command.CommandType = CommandType.Text;
                command.CommandText = @"" + databaseConnectionSetupId + @"


                            ";
                _context.Database.OpenConnection();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RunMasterFileSynchronizationScript");
            }
        }
    }
}
