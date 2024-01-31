using CompanyPL.ProjectPL.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.ExcelProcessor.Models;
using CompanyPL.ProjectPL.ExcelProcessor.Helper;


namespace CompanyPL.ProjectPL.ExcelProcessor.CustomValidation
{
    public static class HealthDeclarationValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var employeeId = rowValue[nameof(HealthDeclarationState.EmployeeId)]?.ToString();
			var employee = await context.Employee.Where(l => l.EmployeeCode == employeeId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(employee == null) {
				errorValidation += $"Employee is not valid.; ";
			}
			else
			{
				rowValue[nameof(HealthDeclarationState.EmployeeId)] = employee?.Id;
			}
			
			var vaccine = rowValue[nameof(HealthDeclarationState.Vaccine)]?.ToString();
			if (!string.IsNullOrEmpty(vaccine))
			{
				var vaccineMaxLength = 255;
				if (vaccine.Length > vaccineMaxLength)
				{
					errorValidation += $"Vaccine should be less than {vaccineMaxLength} characters.;";
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
																				
			};
			return listOfKeys.Count > 0 ? DictionaryHelper.FindDuplicateRowNumbersPerKey(records, listOfKeys) : new Dictionary<string, HashSet<int>>();
		}
    }
}
