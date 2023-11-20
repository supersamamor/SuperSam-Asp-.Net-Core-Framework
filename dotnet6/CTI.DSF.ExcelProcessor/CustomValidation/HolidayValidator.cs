using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;


namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class HolidayValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            
			var holidayName = rowValue[nameof(HolidayState.HolidayName)]?.ToString();
			if (!string.IsNullOrEmpty(holidayName))
			{
				var holidayNameMaxLength = 255;
				if (holidayName.Length > holidayNameMaxLength)
				{
					errorValidation += $"Holiday Name should be less than {holidayNameMaxLength} characters.;";
				}
				if (await context.Holiday.Where(l => l.HolidayName == holidayName).AsNoTracking().IgnoreQueryFilters().AnyAsync())
				{
					errorValidation += $"Holiday Name already exist from the database.";
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
								nameof(HolidayState.HolidayName),
								
			};
			return listOfKeys.Count > 0 ? DictionaryHelper.FindDuplicateRowNumbersPerKey(records, listOfKeys) : new Dictionary<string, HashSet<int>>();
		}
    }
}
