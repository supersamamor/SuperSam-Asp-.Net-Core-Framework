using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;


namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class CompanyValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var companyCode = rowValue[nameof(CompanyState.CompanyCode)]?.ToString();
			if (!string.IsNullOrEmpty(companyCode))
			{
				var companyCodeMaxLength = 450;
				if (companyCode.Length > companyCodeMaxLength)
				{
					errorValidation += $"Company Code should be less than {companyCodeMaxLength} characters.;";
				}
				if (await context.Company.Where(l => l.CompanyCode == companyCode).AsNoTracking().IgnoreQueryFilters().AnyAsync())
				{
					errorValidation += $"Company Code already exist from the database.";
				}
			}
			var companyName = rowValue[nameof(CompanyState.CompanyName)]?.ToString();
			if (!string.IsNullOrEmpty(companyName))
			{
				var companyNameMaxLength = 450;
				if (companyName.Length > companyNameMaxLength)
				{
					errorValidation += $"Company Name should be less than {companyNameMaxLength} characters.;";
				}
				if (await context.Company.Where(l => l.CompanyName == companyName).AsNoTracking().IgnoreQueryFilters().AnyAsync())
				{
					errorValidation += $"Company Name already exist from the database.";
				}
			}
			
			
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
				nameof(CompanyState.CompanyCode),
				nameof(CompanyState.CompanyName),
												
			};
			return listOfKeys.Count > 0 ? DictionaryHelper.FindDuplicateRowNumbersPerKey(records, listOfKeys) : new Dictionary<string, HashSet<int>>();
		}
    }
}
