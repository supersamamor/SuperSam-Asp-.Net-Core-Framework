using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;

namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class DepartmentValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidateAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
            var companyCode = rowValue[nameof(DepartmentState.CompanyCode)]?.ToString();
			var company = await context.Company.Where(l => l.CompanyCode == companyCode).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(company == null) {
				throw new Exception("Company Code is not valid.");
			}
			rowValue[nameof(DepartmentState.CompanyCode)] = company?.Id;			
            return rowValue;
        }
    }
}
