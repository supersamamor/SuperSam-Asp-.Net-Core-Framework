using CompanyPL.ProjectPL.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.ExcelProcessor.Models;
using CompanyPL.ProjectPL.ExcelProcessor.Helper;


namespace CompanyPL.ProjectPL.ExcelProcessor.CustomValidation
{
    public static class SampleParentValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var name = rowValue[nameof(SampleParentState.Name)]?.ToString();
			if (!string.IsNullOrEmpty(name))
			{
				var nameMaxLength = 255;
				if (name.Length > nameMaxLength)
				{
					errorValidation += $"Name should be less than {nameMaxLength} characters.;";
				}
				if (await context.SampleParent.Where(l => l.Name == name).AsNoTracking().IgnoreQueryFilters().AnyAsync())
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
				nameof(SampleParentState.Name),
								
			};
			return listOfKeys.Count > 0 ? DictionaryHelper.FindDuplicateRowNumbersPerKey(records, listOfKeys) : new Dictionary<string, HashSet<int>>();
		}
    }
}
