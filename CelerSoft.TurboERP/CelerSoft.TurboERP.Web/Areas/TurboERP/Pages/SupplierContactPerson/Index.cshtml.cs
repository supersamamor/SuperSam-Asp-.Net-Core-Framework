using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierContactPerson.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.SupplierContactPerson;

[Authorize(Policy = Permission.SupplierContactPerson.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public SupplierContactPersonViewModel SupplierContactPerson { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetSupplierContactPersonQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                SupplierId = e.Supplier?.Company,
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
        var result = await Mediatr.Send(request.ToQuery<GetSupplierContactPersonQuery>(nameof(SupplierContactPersonState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
