using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record DeliveryViewModel : BaseViewModel
{	
	[Display(Name = "Delivery Code")]
	[Required]
	public string DeliveryCode { get; init; } = "";

	public string? ForeignKeyAssignment { get; set; }

    [Display(Name = "Assignment Code")]
    [Required]
    public string AssignmentCode { get; init; } = "";

    [Display(Name = "Task Description")]
    [Required]
    public string? TaskDescription { get; init; }

	[Display(Name = "Due Date")]
	public DateTime? DueDate { get; init; } = DateTime.Now.Date;
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
	[Display(Name = "Status")]
	
	public string? Status { get; init; }

    [Display(Name = "Remarks")]

    public string? Remarks { get; init; }

	[Display(Name = "Sub Tak")]
	public string? SubTask { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public AssignmentViewModel? Assignment { get; init; }
		
	
}
