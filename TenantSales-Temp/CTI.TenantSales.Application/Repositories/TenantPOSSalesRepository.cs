using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CTI.TenantSales.Application.Repositories
{
    public class TenantPOSSalesRepository
    {
        private readonly ApplicationContext _context;
        private readonly decimal _taxRate;
        private readonly decimal _salesAmountThreshold;
        public TenantPOSSalesRepository(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _taxRate = configuration.GetValue<decimal>("TaxRate");
            _salesAmountThreshold = configuration.GetValue<decimal>("SalesAmountThreshold");
        }
        public async Task<TenantPOSSalesState?> GetPreviousDaySales(TenantPOSSalesState currentSales)
        {
            return await _context.TenantPOSSales.IgnoreQueryFilters()
                 .Where(l => l.TenantPOSId == currentSales.TenantPOSId && l.SalesType == currentSales.SalesType && l.SalesDate == currentSales.SalesDate.AddDays(-1) && l.SalesCategory == currentSales.SalesCategory!.Trim() && l.HourCode == currentSales.HourCode)
                 .AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<string?> GetTenantPOSEntity(string posId)
        {
            return (await _context.TenantPOS.IgnoreQueryFilters()
                 .Where(l => l.Id == posId).AsNoTracking().FirstOrDefaultAsync())!.Entity;
        }
        public async Task UpdateTenantPOSSales(TenantPOSSalesState sale)
        {
            //Get Previous Sales
            var previousSale = await GetPreviousDaySales(sale);
            //Validate Daily Sales      
            sale.ProcessPreviousDaySales(previousSale, _taxRate, _salesAmountThreshold);
            _context.Update(sale);
            await UpdateSucceedingDaysAutocalculatedTotalAccumulatedSales(sale);
        }
        private async Task UpdateSucceedingDaysAutocalculatedTotalAccumulatedSales(TenantPOSSalesState sale)
        {
            //Get Previous Sales
            var previousSale = await GetPreviousDaySales(sale);
            //Validate Daily Sales      
            sale.ProcessPreviousDaySales(previousSale, _taxRate, _salesAmountThreshold);
            _context.Update(sale);
            int day = 1;
            var prevSales = sale;
            var nextDay = await GetNextDaySales(sale, day);
            while (nextDay != null)
            {
                nextDay.ProcessPreviousDaySales(prevSales, _taxRate, _salesAmountThreshold);
                _context.Update(nextDay);
                day += 1;
                prevSales = nextDay;
                nextDay = await GetNextDaySales(sale, day);
            }
        }
        private async Task<TenantPOSSalesState?> GetNextDaySales(TenantPOSSalesState currentSales, int dayBuffer)
        {
            return await _context.TenantPOSSales.IgnoreQueryFilters()
                 .Where(l => l.TenantPOSId == currentSales.TenantPOSId && l.SalesType == currentSales.SalesType && l.SalesDate == currentSales.SalesDate.AddDays(dayBuffer) && l.SalesCategory == currentSales.SalesCategory!.Trim() && l.HourCode == currentSales.HourCode)
                 .AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
