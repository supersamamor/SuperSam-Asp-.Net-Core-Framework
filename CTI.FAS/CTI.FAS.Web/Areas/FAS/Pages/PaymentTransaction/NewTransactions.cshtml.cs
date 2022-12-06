using CTI.FAS.Application.Features.FAS.PaymentTransaction.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Pages.PaymentTransaction;

[Authorize(Policy = Permission.PaymentTransaction.View)]
public class NewTransactionsModel : BasePageModel<NewTransactionsModel>
{
    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }
    public PaymentTransactionTabNavigationPartial PaymentTransactionTabNavigation { get; set; } = new() { TabName = Constants.PaymentTransactionTabNavigation.New };
    [BindProperty]
    [Required]
    public string? Entity { get; set; }
    [BindProperty]
    [Required]
    public string? PaymentType { get; set; }
    [BindProperty]
    [Required]
    public string? AccountTransaction { get; set; }
    [BindProperty]
    [Required]
    public DateTime? DateFrom { get; set; }
    [BindProperty]
    [Required]
    public DateTime? DateTo { get; set; }
    public IActionResult OnGet()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return Page();
    }
    public IActionResult OnPost()
    {
      
        return Page();
    }
    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetPaymentTransactionQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                EnrolledPayeeId = e.EnrolledPayee?.Id,
				BatchId = e.Batch?.Id,
				TransmissionDate = e.TransmissionDate?.ToString("MMM dd, yyyy HH:mm"),
				e.DocumentNumber,
				DocumentDate = e.DocumentDate.ToString("MMM dd, yyyy HH:mm"),
				DocumentAmount = e.DocumentAmount.ToString("##,##.00"),
				e.CheckNumber,
				e.PaymentType,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetPaymentTransactionQuery>(nameof(PaymentTransactionState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
