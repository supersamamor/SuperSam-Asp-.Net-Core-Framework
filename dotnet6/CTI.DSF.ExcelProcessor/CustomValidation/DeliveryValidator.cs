using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;


namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class DeliveryValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var taskCompanyAssignmentId = rowValue[nameof(DeliveryState.TaskCompanyAssignmentId)]?.ToString();
			var taskCompanyAssignment = await context.TaskCompanyAssignment.Where(l => l.Id == taskCompanyAssignmentId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(taskCompanyAssignment == null) {
				errorValidation += $"Task / Company Assignment is not valid.; ";
			}
			else
			{
				rowValue[nameof(DeliveryState.TaskCompanyAssignmentId)] = taskCompanyAssignment?.Id;
			}
			if (!string.IsNullOrEmpty(taskCompanyAssignmentId))
			{
				var taskCompanyAssignmentIdMaxLength = 450;
				if (taskCompanyAssignmentId.Length > taskCompanyAssignmentIdMaxLength)
				{
					errorValidation += $"Task / Company Assignment should be less than {taskCompanyAssignmentIdMaxLength} characters.;";
				}
			}
			var deliveryCode = rowValue[nameof(DeliveryState.DeliveryCode)]?.ToString();
			if (!string.IsNullOrEmpty(deliveryCode))
			{
				var deliveryCodeMaxLength = 450;
				if (deliveryCode.Length > deliveryCodeMaxLength)
				{
					errorValidation += $"Delivery Code should be less than {deliveryCodeMaxLength} characters.;";
				}
			}
			var assignmentId = rowValue[nameof(DeliveryState.AssignmentId)]?.ToString();
			var assignment = await context.Assignment.Where(l => l.Id == assignmentId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(assignment == null) {
				errorValidation += $"Assignment Id is not valid.; ";
			}
			else
			{
				rowValue[nameof(DeliveryState.AssignmentId)] = assignment?.Id;
			}
			if (!string.IsNullOrEmpty(assignmentId))
			{
				var assignmentIdMaxLength = 450;
				if (assignmentId.Length > assignmentIdMaxLength)
				{
					errorValidation += $"Assignment Id should be less than {assignmentIdMaxLength} characters.;";
				}
			}
			
			var status = rowValue[nameof(DeliveryState.Status)]?.ToString();
			if (!string.IsNullOrEmpty(status))
			{
				var statusMaxLength = 50;
				if (status.Length > statusMaxLength)
				{
					errorValidation += $"Status should be less than {statusMaxLength} characters.;";
				}
			}
			
			
			
			
			var submittedBy = rowValue[nameof(DeliveryState.SubmittedBy)]?.ToString();
			if (!string.IsNullOrEmpty(submittedBy))
			{
				var submittedByMaxLength = 450;
				if (submittedBy.Length > submittedByMaxLength)
				{
					errorValidation += $"Submitted By should be less than {submittedByMaxLength} characters.;";
				}
			}
			
			var reviewedBy = rowValue[nameof(DeliveryState.ReviewedBy)]?.ToString();
			if (!string.IsNullOrEmpty(reviewedBy))
			{
				var reviewedByMaxLength = 450;
				if (reviewedBy.Length > reviewedByMaxLength)
				{
					errorValidation += $"Reviewed By should be less than {reviewedByMaxLength} characters.;";
				}
			}
			
			var approvedBy = rowValue[nameof(DeliveryState.ApprovedBy)]?.ToString();
			if (!string.IsNullOrEmpty(approvedBy))
			{
				var approvedByMaxLength = 450;
				if (approvedBy.Length > approvedByMaxLength)
				{
					errorValidation += $"Approved By should be less than {approvedByMaxLength} characters.;";
				}
			}
			
			var rejectedBy = rowValue[nameof(DeliveryState.RejectedBy)]?.ToString();
			if (!string.IsNullOrEmpty(rejectedBy))
			{
				var rejectedByMaxLength = 450;
				if (rejectedBy.Length > rejectedByMaxLength)
				{
					errorValidation += $"Rejected By should be less than {rejectedByMaxLength} characters.;";
				}
			}
			
			var cancelledBy = rowValue[nameof(DeliveryState.CancelledBy)]?.ToString();
			if (!string.IsNullOrEmpty(cancelledBy))
			{
				var cancelledByMaxLength = 450;
				if (cancelledBy.Length > cancelledByMaxLength)
				{
					errorValidation += $"Cancelled By should be less than {cancelledByMaxLength} characters.;";
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
