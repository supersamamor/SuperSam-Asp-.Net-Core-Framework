using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;


namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class TaskMasterValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var taskNo = rowValue[nameof(TaskMasterState.TaskNo)]?.ToString();
			var taskDescription = rowValue[nameof(TaskMasterState.TaskDescription)]?.ToString();
			if (!string.IsNullOrEmpty(taskDescription))
			{
				var taskDescriptionMaxLength = 255;
				if (taskDescription.Length > taskDescriptionMaxLength)
				{
					errorValidation += $"Task Description should be less than {taskDescriptionMaxLength} characters.;";
				}
			}
			var taskClassification = rowValue[nameof(TaskMasterState.TaskClassification)]?.ToString();
			var taskFrequency = rowValue[nameof(TaskMasterState.TaskFrequency)]?.ToString();
			var taskDueDay = rowValue[nameof(TaskMasterState.TaskDueDay)]?.ToString();
			var targetDueDate = rowValue[nameof(TaskMasterState.TargetDueDate)]?.ToString();
			var holidayTag = rowValue[nameof(TaskMasterState.HolidayTag)]?.ToString();
			var active = rowValue[nameof(TaskMasterState.Active)]?.ToString();
			
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
