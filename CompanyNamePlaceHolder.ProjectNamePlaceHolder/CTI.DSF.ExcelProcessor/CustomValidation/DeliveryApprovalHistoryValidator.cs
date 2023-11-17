using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;


namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class DeliveryApprovalHistoryValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var deliveryId = rowValue[nameof(DeliveryApprovalHistoryState.DeliveryId)]?.ToString();
			if (!string.IsNullOrEmpty(deliveryId))
			{
				var deliveryIdMaxLength = 450;
				if (deliveryId.Length > deliveryIdMaxLength)
				{
					errorValidation += $"Delivery should be less than {deliveryIdMaxLength} characters.;";
				}
			}
			var transactionDateTime = rowValue[nameof(DeliveryApprovalHistoryState.TransactionDateTime)]?.ToString();
			var status = rowValue[nameof(DeliveryApprovalHistoryState.Status)]?.ToString();
			if (!string.IsNullOrEmpty(status))
			{
				var statusMaxLength = 50;
				if (status.Length > statusMaxLength)
				{
					errorValidation += $"Status should be less than {statusMaxLength} characters.;";
				}
			}
			var transactedBy = rowValue[nameof(DeliveryApprovalHistoryState.TransactedBy)]?.ToString();
			if (!string.IsNullOrEmpty(transactedBy))
			{
				var transactedByMaxLength = 450;
				if (transactedBy.Length > transactedByMaxLength)
				{
					errorValidation += $"Transacted By should be less than {transactedByMaxLength} characters.;";
				}
			}
			var remarks = rowValue[nameof(DeliveryApprovalHistoryState.Remarks)]?.ToString();
			
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
