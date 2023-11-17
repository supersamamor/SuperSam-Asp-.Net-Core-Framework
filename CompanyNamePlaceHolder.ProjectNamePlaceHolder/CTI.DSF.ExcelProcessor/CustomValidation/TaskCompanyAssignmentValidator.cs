using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;


namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class TaskCompanyAssignmentValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var taskMasterId = rowValue[nameof(TaskCompanyAssignmentState.TaskMasterId)]?.ToString();
			var companyId = rowValue[nameof(TaskCompanyAssignmentState.CompanyId)]?.ToString();
			var company = await context.Company.Where(l => l.CompanyName == companyId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(company == null) {
				errorValidation += $"Company is not valid.; ";
			}
			else
			{
				rowValue[nameof(TaskCompanyAssignmentState.CompanyId)] = company?.Id;
			}
			var departmentId = rowValue[nameof(TaskCompanyAssignmentState.DepartmentId)]?.ToString();
			var sectionId = rowValue[nameof(TaskCompanyAssignmentState.SectionId)]?.ToString();
			var teamId = rowValue[nameof(TaskCompanyAssignmentState.TeamId)]?.ToString();
			
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
