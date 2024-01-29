using CompanyPL.ProjectPL.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.ExcelProcessor.Models;
using CompanyPL.ProjectPL.ExcelProcessor.Helper;


namespace CompanyPL.ProjectPL.ExcelProcessor.CustomValidation
{
    public static class EmployeeValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var firstName = rowValue[nameof(EmployeeState.FirstName)]?.ToString();
			if (!string.IsNullOrEmpty(firstName))
			{
				var firstNameMaxLength = 255;
				if (firstName.Length > firstNameMaxLength)
				{
					errorValidation += $"First Name should be less than {firstNameMaxLength} characters.;";
				}
			}
			var employeeCode = rowValue[nameof(EmployeeState.EmployeeCode)]?.ToString();
			if (!string.IsNullOrEmpty(employeeCode))
			{
				var employeeCodeMaxLength = 255;
				if (employeeCode.Length > employeeCodeMaxLength)
				{
					errorValidation += $"Employee Code should be less than {employeeCodeMaxLength} characters.;";
				}
				if (await context.Employee.Where(l => l.EmployeeCode == employeeCode).AsNoTracking().IgnoreQueryFilters().AnyAsync())
				{
					errorValidation += $"Employee Code already exist from the database.";
				}
			}
			var lastName = rowValue[nameof(EmployeeState.LastName)]?.ToString();
			if (!string.IsNullOrEmpty(lastName))
			{
				var lastNameMaxLength = 255;
				if (lastName.Length > lastNameMaxLength)
				{
					errorValidation += $"Last Name should be less than {lastNameMaxLength} characters.;";
				}
			}
			var middleName = rowValue[nameof(EmployeeState.MiddleName)]?.ToString();
			if (!string.IsNullOrEmpty(middleName))
			{
				var middleNameMaxLength = 255;
				if (middleName.Length > middleNameMaxLength)
				{
					errorValidation += $"Middle Name should be less than {middleNameMaxLength} characters.;";
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
								nameof(EmployeeState.EmployeeCode),
																
			};
			return listOfKeys.Count > 0 ? DictionaryHelper.FindDuplicateRowNumbersPerKey(records, listOfKeys) : new Dictionary<string, HashSet<int>>();
		}
    }
}
