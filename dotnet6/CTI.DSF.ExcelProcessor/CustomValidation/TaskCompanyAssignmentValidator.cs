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
			var taskMaster = await context.TaskMaster.Where(l => l.Id == taskMasterId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(taskMaster == null) {
				errorValidation += $"Task Master is not valid.; ";
			}
			else
			{
				rowValue[nameof(TaskCompanyAssignmentState.TaskMasterId)] = taskMaster?.Id;
			}
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
			var department = await context.Department.Where(l => l.Id == departmentId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(department == null) {
				errorValidation += $"Department is not valid.; ";
			}
			else
			{
				rowValue[nameof(TaskCompanyAssignmentState.DepartmentId)] = department?.Id;
			}
			var sectionId = rowValue[nameof(TaskCompanyAssignmentState.SectionId)]?.ToString();
			var section = await context.Section.Where(l => l.Id == sectionId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(section == null) {
				errorValidation += $"Section is not valid.; ";
			}
			else
			{
				rowValue[nameof(TaskCompanyAssignmentState.SectionId)] = section?.Id;
			}
			var teamId = rowValue[nameof(TaskCompanyAssignmentState.TeamId)]?.ToString();
			var team = await context.Team.Where(l => l.Id == teamId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(team == null) {
				errorValidation += $"Team is not valid.; ";
			}
			else
			{
				rowValue[nameof(TaskCompanyAssignmentState.TeamId)] = team?.Id;
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
