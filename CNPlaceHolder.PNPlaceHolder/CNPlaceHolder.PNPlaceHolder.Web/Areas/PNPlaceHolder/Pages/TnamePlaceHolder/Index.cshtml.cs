using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Queries;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Models;
using CNPlaceHolder.PNPlaceHolder.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Pages.TnamePlaceHolder;

[Authorize(Policy = Permission.TnamePlaceHolder.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public TnamePlaceHolderViewModel TnamePlaceHolder { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetTnamePlaceHolderQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Colname,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetTnamePlaceHolderQuery>(nameof(TnamePlaceHolderState.Colname)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Colname }));
    }
}
