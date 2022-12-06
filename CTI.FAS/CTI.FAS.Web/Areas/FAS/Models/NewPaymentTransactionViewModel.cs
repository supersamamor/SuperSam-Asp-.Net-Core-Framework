using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record NewPaymentTransactionViewModel : BaseViewModel
{	
	public string?  ForeignKeyEnrolledPayee { get; set; }	
	public string?  ForeignKeyBatch { get; set; }
	public string DocumentNumber { get; init; } = "";	
	public DateTime DocumentDate { get; init; } = DateTime.Now.Date;
	public decimal DocumentAmount { get; init; }	
	public string PaymentType { get; init; } = "";	
	public string Status { get; init; } = "";	
	public DateTime LastModifiedDate { get; set; }
	public string AccountTransaction { get; init; } = "";
	public bool Enabled { get; set; }
}
