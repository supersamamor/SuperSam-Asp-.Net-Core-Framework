using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record DeliveryViewModel : BaseViewModel
{	
	[Display(Name = "Delivery Code")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? DeliveryCode { get; init; }
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
	
	public DateTime LastModifiedDate { get; set; }
	public AssignmentViewModel? Assignment { get; init; }
		
	
}
