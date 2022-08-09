using AutoMapper;
using CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Commands;
using CTI.TenantSales.Core.Constants;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using CTI.TenantSales.Scheduler.Helper;
using CTI.TenantSales.Scheduler.Models;
using CTI.TenantSales.Scheduler.Repository;
using CTI.TenantSales.Scheduler.Repository.DataSynchronizationRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Data;

namespace CTI.TenantSales.Scheduler.Jobs
{
    public class ReportDataSynchronizationJob : IJob
    {
        private readonly ApplicationContext _context;
        private readonly ReportDataSynchronizationRepository _reportDataSynchronizationRepository;
        public ReportDataSynchronizationJob(ApplicationContext context, ReportDataSynchronizationRepository reportDataSynchronizationRepository)
        {
            _context = context;       
            _reportDataSynchronizationRepository = reportDataSynchronizationRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await ProcessReportDataSynchronization();
        }
        private async Task ProcessReportDataSynchronization()
        {
            var projectList = await _context.DatabaseConnectionSetup.Where(l => l.IsDisabled == false)
                .SelectMany(l => l.CompanyList!).Where(l => l.IsDisabled == false)
                .SelectMany(l => l.ProjectList!).Where(l => l.IsDisabled == false && l.Name != "N/A")
                .Include(l=>l.Company).ThenInclude(l=>l!.DatabaseConnectionSetup).AsNoTracking().IgnoreQueryFilters().ToListAsync();
            foreach (var projectItem in projectList)
            {
                await _reportDataSynchronizationRepository.RunReportDataSynchronizationScript(projectItem);
            }
        }
    }
}
