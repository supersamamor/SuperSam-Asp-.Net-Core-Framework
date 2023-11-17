using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;


namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class TaskApproverValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var approverUserId = rowValue[nameof(TaskApproverState.ApproverUserId)]?.ToString();
			if (!string.IsNullOrEmpty(approverUserId))
			{
				var approverUserIdMaxLength = 450;
				if (approverUserId.Length > approverUserIdMaxLength)
				{
					errorValidation += $"Approver should be less than {approverUserIdMaxLength} characters.;";
				}
			}
			var taskCompanyAssignmentId = rowValue[nameof(TaskApproverState.TaskCompanyAssignmentId)]?.ToString();
			var approverType = rowValue[nameof(TaskApproverState.ApproverType)]?.ToString();
			var isPrimary = rowValue[nameof(TaskApproverState.IsPrimary)]?.ToString();
			var sequence = rowValue[nameof(TaskApproverState.Sequence)]?.ToString();
			
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
