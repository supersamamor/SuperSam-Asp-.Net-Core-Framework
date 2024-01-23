using CompanyPL.ProjectPL.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.ExcelProcessor.Models;
using CompanyPL.ProjectPL.ExcelProcessor.Helper;


namespace CompanyPL.ProjectPL.ExcelProcessor.CustomValidation
{
    public static class ContactInformationValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var employeeId = rowValue[nameof(ContactInformationState.EmployeeId)]?.ToString();
			var employee = await context.Employee.Where(l => l.EmployeeCode == employeeId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(employee == null) {
				errorValidation += $"Employee is not valid.; ";
			}
			else
			{
				rowValue[nameof(ContactInformationState.EmployeeId)] = employee?.Id;
			}
			var contactDetails = rowValue[nameof(ContactInformationState.ContactDetails)]?.ToString();
			if (!string.IsNullOrEmpty(contactDetails))
			{
				var contactDetailsMaxLength = 255;
				if (contactDetails.Length > contactDetailsMaxLength)
				{
					errorValidation += $"Contact Details should be less than {contactDetailsMaxLength} characters.;";
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
