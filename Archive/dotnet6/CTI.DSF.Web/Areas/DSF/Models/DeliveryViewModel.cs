using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record DeliveryViewModel : BaseViewModel
{	
	[Display(Name = "Task / Company Assignment")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TaskCompanyAssignmentId { get; init; } = "";
	public string?  ReferenceFieldTaskCompanyAssignmentId { get; set; }
	[Display(Name = "Delivery Code")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DeliveryCode { get; init; } = "";
	[Display(Name = "Assignment Id")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AssignmentId { get; init; } = "";
	public string?  ReferenceFieldAssignmentId { get; set; }
	[Display(Name = "Due Date")]
	[Required]
	public DateTime DueDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Status")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get; init; } = "";
	[Display(Name = "Delivery Attachment")]
	[Required]
	
	public IFormFile? DeliveryAttachmentForm { get; set; }public string? DeliveryAttachment { get; init; } = "";
	public string? GeneratedDeliveryAttachmentPath
	{
		get
		{
			return this.DeliveryAttachmentForm?.FileName == null ? this.DeliveryAttachment : "\\" + WebConstants.Delivery + "\\" + this.Id + "\\" + nameof(this.DeliveryAttachment) + "\\" + this.DeliveryAttachmentForm!.FileName;
		}
	}
	[Display(Name = "Remarks")]
	[Required]
	
	public string Remarks { get; init; } = "";
	[Display(Name = "Holiday Tag")]
	[Required]
	
	public string HolidayTag { get; init; } = "";
	[Display(Name = "Submitted Date")]
	public DateTime? SubmittedDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Submitted By")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SubmittedBy { get; init; }
	[Display(Name = "Reviewed Date")]
	public DateTime? ReviewedDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Reviewed By")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ReviewedBy { get; init; }
	[Display(Name = "Approved Date")]
	public DateTime? ApprovedDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Approved By")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ApprovedBy { get; init; }
	[Display(Name = "Rejected Date")]
	public DateTime? RejectedDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Rejected By")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? RejectedBy { get; init; }
	[Display(Name = "Cancelled Date")]
	public DateTime? CancelledDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Cancelled By")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? CancelledBy { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public TaskCompanyAssignmentViewModel? TaskCompanyAssignment { get; init; }
	public AssignmentViewModel? Assignment { get; init; }
		
	public IList<DeliveryApprovalHistoryViewModel>? DeliveryApprovalHistoryList { get; set; }
	
}
