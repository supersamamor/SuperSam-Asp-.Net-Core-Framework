using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record GeneratedViewModel : BaseViewModel
{	
	[Display(Name = "Entity")]
	[Required]
	[StringLength(4, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CompanyId { get; init; } = "";
	public string?  ForeignKeyCompany { get; set; }
	[Display(Name = "Creditor")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CreditorId { get; init; } = "";
	public string?  ForeignKeyCreditor { get; set; }
	[Display(Name = "Batch")]
	[Required]
	
	public string BatchId { get; init; } = "";
	public string?  ForeignKeyBatch { get; set; }
	[Display(Name = "Name")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	[Display(Name = "Transmission Date")]
	[Required]
	public DateTime TransmissionDate { get; init; } = DateTime.Now.Date;
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
	[Display(Name = "Release")]
	[Required]
	[StringLength(1, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Release { get; init; } = "";
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
	[StringLength(9, ErrorMessage = "{0} length can't be more than {1}.")]
	public string GroupCode { get; init; } = "";
	[Display(Name = "Group")]
	[Required]
	[StringLength(9, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public CompanyViewModel? Company { get; init; }
	public CreditorViewModel? Creditor { get; init; }
	public BatchViewModel? Batch { get; init; }
		
	
}
