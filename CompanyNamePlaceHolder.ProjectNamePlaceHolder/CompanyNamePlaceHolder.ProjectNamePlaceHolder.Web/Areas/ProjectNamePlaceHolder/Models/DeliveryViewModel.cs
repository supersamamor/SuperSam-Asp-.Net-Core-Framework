using CompanyNamePlaceHolder.Common.Web.Utility.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;

public record DeliveryViewModel : BaseViewModel
{	
	[Display(Name = "ApproverRemarks")]
	
	public string? ApproverRemarks { get; init; }
	[Display(Name = "Status")]
	
	public string? Status { get; init; }
	[Display(Name = "EndorserRemarks")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EndorserRemarks { get; init; }
	[Display(Name = "Endorsed Date")]
	public DateTime? EndorsedDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Approved Tag")]
	
	public string? ApprovedTag { get; init; }
	[Display(Name = "Endorsed Tag")]
	
	public string? EndorsedTag { get; init; }
	[Display(Name = "Delivery Code")]
	[Required]
	
	public string DeliveryCode { get; init; } = "";
	[Display(Name = "Actual Delivery Date")]
	[Required]
	public DateTime ActualDeliveryDate { get; init; } = DateTime.Now.Date;
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
	[Display(Name = "Actual Delivery Remarks")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ActualDeliveryRemarks { get; init; }
	[Display(Name = "Assignment Code")]
	[Required]
	
	public string AssignmentCode { get; init; } = "";
	public string?  ReferenceFieldAssignmentCode { get; set; }
	[Display(Name = "Due Date")]
	public DateTime? DueDate { get; init; } = DateTime.Now.Date;
	
	public DateTime LastModifiedDate { get; set; }
	public AssignmentViewModel? Assignment { get; init; }
		
	
}
