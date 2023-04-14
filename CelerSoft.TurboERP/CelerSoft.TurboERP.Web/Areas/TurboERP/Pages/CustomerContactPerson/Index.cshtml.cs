using CelerSoft.TurboERP.Application.Features.TurboERP.CustomerContactPerson.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.CustomerContactPerson;

[Authorize(Policy = Permission.CustomerContactPerson.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public CustomerContactPersonViewModel CustomerContactPerson { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetCustomerContactPersonQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                CustomerId = e.Customer?.TINNumber,
				e.FullName,
				e.Position,
				e.Email,
				e.MobileNumber,
				e.PhoneNumber,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetCustomerContactPersonQuery>(nameof(CustomerContactPersonState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
