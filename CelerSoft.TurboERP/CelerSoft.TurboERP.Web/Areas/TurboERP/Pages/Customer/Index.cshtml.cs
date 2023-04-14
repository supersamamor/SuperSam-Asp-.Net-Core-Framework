using CelerSoft.TurboERP.Application.Features.TurboERP.Customer.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Customer;

[Authorize(Policy = Permission.Customer.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public CustomerViewModel Customer { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetCustomerQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Company,
				e.TINNumber,
				e.Address,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetCustomerQuery>(nameof(CustomerState.TINNumber)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.TINNumber }));
    }
}
