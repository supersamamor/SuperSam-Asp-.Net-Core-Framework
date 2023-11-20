using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;


namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class AssignmentValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var assignmentCode = rowValue[nameof(AssignmentState.AssignmentCode)]?.ToString();
			if (!string.IsNullOrEmpty(assignmentCode))
			{
				var assignmentCodeMaxLength = 450;
				if (assignmentCode.Length > assignmentCodeMaxLength)
				{
					errorValidation += $"Assignment Code should be less than {assignmentCodeMaxLength} characters.;";
				}
			}
			var taskCompanyAssignmentId = rowValue[nameof(AssignmentState.TaskCompanyAssignmentId)]?.ToString();
			var taskCompanyAssignment = await context.TaskCompanyAssignment.Where(l => l.Id == taskCompanyAssignmentId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(taskCompanyAssignment == null) {
				errorValidation += $"Task / Company Assignment is not valid.; ";
			}
			else
			{
				rowValue[nameof(AssignmentState.TaskCompanyAssignmentId)] = taskCompanyAssignment?.Id;
			}
			if (!string.IsNullOrEmpty(taskCompanyAssignmentId))
			{
				var taskCompanyAssignmentIdMaxLength = 450;
				if (taskCompanyAssignmentId.Length > taskCompanyAssignmentIdMaxLength)
				{
					errorValidation += $"Task / Company Assignment should be less than {taskCompanyAssignmentIdMaxLength} characters.;";
				}
			}
			var primaryAssignee = rowValue[nameof(AssignmentState.PrimaryAssignee)]?.ToString();
			if (!string.IsNullOrEmpty(primaryAssignee))
			{
				var primaryAssigneeMaxLength = 450;
				if (primaryAssignee.Length > primaryAssigneeMaxLength)
				{
					errorValidation += $"Primary Assignee should be less than {primaryAssigneeMaxLength} characters.;";
				}
			}
			var alternateAssignee = rowValue[nameof(AssignmentState.AlternateAssignee)]?.ToString();
			if (!string.IsNullOrEmpty(alternateAssignee))
			{
				var alternateAssigneeMaxLength = 450;
				if (alternateAssignee.Length > alternateAssigneeMaxLength)
				{
					errorValidation += $"Alternate Assignee should be less than {alternateAssigneeMaxLength} characters.;";
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
