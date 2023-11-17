using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;


namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class TagsValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var name = rowValue[nameof(TagsState.Name)]?.ToString();
			if (!string.IsNullOrEmpty(name))
			{
				var nameMaxLength = 255;
				if (name.Length > nameMaxLength)
				{
					errorValidation += $"Name should be less than {nameMaxLength} characters.;";
				}
				if (await context.Tags.Where(l => l.Name == name).AsNoTracking().IgnoreQueryFilters().AnyAsync())
				{
					errorValidation += $"Name already exist from the database.";
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
				nameof(TagsState.Name),
								
			};
			return DictionaryHelper.FindDuplicateRowNumbersPerKey(records, listOfKeys);
		}
    }
}
