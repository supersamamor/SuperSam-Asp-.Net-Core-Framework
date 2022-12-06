using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record PaymentTransactionViewModel : BaseViewModel
{	
	[Display(Name = "Enrolled Payee")]
	[Required]
	[StringLength(4, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EnrolledPayeeId { get; init; } = "";
	public string?  ForeignKeyEnrolledPayee { get; set; }
	[Display(Name = "Batch")]
	
	public string? BatchId { get; init; }
	public string?  ForeignKeyBatch { get; set; }
	[Display(Name = "Transmission Date")]
	public DateTime? TransmissionDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Document Number")]
	[Required]
	[StringLength(15, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DocumentNumber { get; init; } = "";
	[Display(Name = "Document Date")]
	[Required]
	public DateTime DocumentDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Document Amount")]
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal DocumentAmount { get; init; }
	[Display(Name = "Check Number")]
	[Required]
	[StringLength(15, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CheckNumber { get; init; } = "";
	[Display(Name = "Payment Type")]
	[Required]
	[StringLength(9, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PaymentType { get; init; } = "";
	[Display(Name = "Text File Name")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TextFileName { get; init; } = "";
	[Display(Name = "Pdf Report")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PdfReport { get; init; } = "";
	[Display(Name = "Emailed")]
	[Required]
	public bool Emailed { get; init; }
	[Display(Name = "Group")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string GroupCode { get; init; } = "";
	[Display(Name = "Status")]
	[Required]
	[StringLength(30, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get; init; } = "";
	[Display(Name = "Ifca Batch Number")]
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal IfcaBatchNumber { get; init; }
	[Display(Name = "Ifca Line Number")]
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal IfcaLineNumber { get; init; }
	[Display(Name = "Email Sent Count")]
	[Required]
	public int EmailSentCount { get; init; }
	[Display(Name = "Email Sent Date / Time")]
	public DateTime? EmailSentDateTime { get; init; } = DateTime.Now.Date;
	[Display(Name = "For Sending")]
	[Required]
	public bool IsForSending { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public EnrolledPayeeViewModel? EnrolledPayee { get; init; }
	public BatchViewModel? Batch { get; init; }
}
