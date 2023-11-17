using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;


namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class SectionValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var departmentCode = rowValue[nameof(SectionState.DepartmentCode)]?.ToString();
			if (!string.IsNullOrEmpty(departmentCode))
			{
				var departmentCodeMaxLength = 450;
				if (departmentCode.Length > departmentCodeMaxLength)
				{
					errorValidation += $"Department Code should be less than {departmentCodeMaxLength} characters.;";
				}
			}
			var sectionCode = rowValue[nameof(SectionState.SectionCode)]?.ToString();
			if (!string.IsNullOrEmpty(sectionCode))
			{
				var sectionCodeMaxLength = 450;
				if (sectionCode.Length > sectionCodeMaxLength)
				{
					errorValidation += $"Section Code should be less than {sectionCodeMaxLength} characters.;";
				}
			}
			var sectionName = rowValue[nameof(SectionState.SectionName)]?.ToString();
			var active = rowValue[nameof(SectionState.Active)]?.ToString();
			
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
