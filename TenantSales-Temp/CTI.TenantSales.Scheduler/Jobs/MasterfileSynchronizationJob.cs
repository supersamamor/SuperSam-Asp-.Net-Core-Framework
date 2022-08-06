using AutoMapper;
using CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Commands;
using CTI.TenantSales.Core.Constants;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using CTI.TenantSales.Scheduler.Helper;
using CTI.TenantSales.Scheduler.Models;
using CTI.TenantSales.Scheduler.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Data;

namespace CTI.TenantSales.Scheduler.Jobs
{
    public class MasterfileSynchronizationJob : IJob
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<MasterfileSynchronizationJob> _logger;
        public MasterfileSynchronizationJob(ApplicationContext context, ILogger<MasterfileSynchronizationJob> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await ProcessMasterFileSynchronization();
        }
        private async Task ProcessMasterFileSynchronization()
        {
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 600;
                command.CommandType = CommandType.Text;
                command.CommandText = @"";
                _context.Database.OpenConnection();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProcessMasterFileSynchronization");
            }
        }
    }
}
