using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record DeliveryApprovalHistoryViewModel : BaseViewModel
{	
	[Display(Name = "Delivery")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DeliveryId { get; init; } = "";
	public string?  ReferenceFieldDeliveryId { get; set; }
	[Display(Name = "Transaction Date/Time")]
	public DateTime? TransactionDateTime { get; init; } = DateTime.Now.Date;
	[Display(Name = "Status")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get; init; }
	[Display(Name = "Transacted By")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? TransactedBy { get; init; }
	[Display(Name = "Remarks")]
	[Required]
	
	public string Remarks { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public DeliveryViewModel? Delivery { get; init; }
		
	
}
