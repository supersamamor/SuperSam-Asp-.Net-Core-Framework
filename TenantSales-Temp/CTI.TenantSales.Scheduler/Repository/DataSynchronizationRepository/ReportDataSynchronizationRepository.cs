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
    public class ReportDataSynchronizationRepository
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ReportDataSynchronizationRepository> _logger;
        public ReportDataSynchronizationRepository(ApplicationContext context, ILogger<ReportDataSynchronizationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task RunReportDataSynchronizationScript(string projectId, int year = 0)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 600;
                command.CommandType = CommandType.Text;
                command.CommandText = @"select " + projectId + @"," + year + @"


                            ";
                _context.Database.OpenConnection();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RunReportDataSynchronizationScript");
            }
        }
    }
}
