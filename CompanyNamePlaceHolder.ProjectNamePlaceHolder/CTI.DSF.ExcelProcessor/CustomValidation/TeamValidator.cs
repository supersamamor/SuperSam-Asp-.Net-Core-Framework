using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;


namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class TeamValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var sectionCode = rowValue[nameof(TeamState.SectionCode)]?.ToString();
			if (!string.IsNullOrEmpty(sectionCode))
			{
				var sectionCodeMaxLength = 450;
				if (sectionCode.Length > sectionCodeMaxLength)
				{
					errorValidation += $"Section Code should be less than {sectionCodeMaxLength} characters.;";
				}
			}
			var teamCode = rowValue[nameof(TeamState.TeamCode)]?.ToString();
			if (!string.IsNullOrEmpty(teamCode))
			{
				var teamCodeMaxLength = 450;
				if (teamCode.Length > teamCodeMaxLength)
				{
					errorValidation += $"Team Code should be less than {teamCodeMaxLength} characters.;";
				}
			}
			var teamName = rowValue[nameof(TeamState.TeamName)]?.ToString();
			var active = rowValue[nameof(TeamState.Active)]?.ToString();
			
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
