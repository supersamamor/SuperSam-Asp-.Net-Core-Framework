using CelerSoft.TurboERP.Application.Features.TurboERP.Item.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Item;

[Authorize(Policy = Permission.Item.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ItemViewModel Item { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetItemQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                ItemTypeId = e.ItemType?.Name,
				e.Code,
				e.Name,
				UnitId = e.Unit?.Name,
				AveragePrice = e.AveragePrice?.ToString("##,##.00"),
				LastPurchasedPrice = e.LastPurchasedPrice?.ToString("##,##.00"),
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetItemQuery>(nameof(ItemState.Code)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Code }));
    }
}
