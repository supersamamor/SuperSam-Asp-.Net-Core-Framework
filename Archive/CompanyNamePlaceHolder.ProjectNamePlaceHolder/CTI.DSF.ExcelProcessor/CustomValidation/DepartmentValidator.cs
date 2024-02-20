using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;


namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class DepartmentValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var companyCode = rowValue[nameof(DepartmentState.CompanyCode)]?.ToString();
			var company = await context.Company.Where(l => l.CompanyName == companyCode).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(company == null) {
				errorValidation += $"Company Code is not valid.; ";
			}
			else
			{
				rowValue[nameof(DepartmentState.CompanyCode)] = company?.Id;
			}
			if (!string.IsNullOrEmpty(companyCode))
			{
				var companyCodeMaxLength = 450;
				if (companyCode.Length > companyCodeMaxLength)
				{
					errorValidation += $"Company Code should be less than {companyCodeMaxLength} characters.;";
				}
			}
			var departmentCode = rowValue[nameof(DepartmentState.DepartmentCode)]?.ToString();
			if (!string.IsNullOrEmpty(departmentCode))
			{
				var departmentCodeMaxLength = 450;
				if (departmentCode.Length > departmentCodeMaxLength)
				{
					errorValidation += $"Department Code should be less than {departmentCodeMaxLength} characters.;";
				}
			}
			var departmentName = rowValue[nameof(DepartmentState.DepartmentName)]?.ToString();
			var active = rowValue[nameof(DepartmentState.Active)]?.ToString();
			
			if (!string.IsNullOrEmpty(errorValidation))
			{
				throw new Exception(errorValidation);
			}
            return rowValue;
        }
			
		public static Dictionary<string, HashSet<int>> DuplicateValidation(List<ExcelRecord> records)
		{
			List<string> listOfKeys = new()
			{
				
				
				
				
								
			};
			return DictionaryHelper.FindDuplicateRowNumbersPerKey(records, listOfKeys);
		}
    }
}
