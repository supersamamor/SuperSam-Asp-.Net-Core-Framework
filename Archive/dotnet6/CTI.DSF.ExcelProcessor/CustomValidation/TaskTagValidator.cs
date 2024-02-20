using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;


namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class TaskTagValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var taskMasterId = rowValue[nameof(TaskTagState.TaskMasterId)]?.ToString();
			var taskMaster = await context.TaskMaster.Where(l => l.Id == taskMasterId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(taskMaster == null) {
				errorValidation += $"Task Master is not valid.; ";
			}
			else
			{
				rowValue[nameof(TaskTagState.TaskMasterId)] = taskMaster?.Id;
			}
			var tagId = rowValue[nameof(TaskTagState.TagId)]?.ToString();
			var tags = await context.Tags.Where(l => l.Name == tagId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(tags == null) {
				errorValidation += $"Tag is not valid.; ";
			}
			else
			{
				rowValue[nameof(TaskTagState.TagId)] = tags?.Id;
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
