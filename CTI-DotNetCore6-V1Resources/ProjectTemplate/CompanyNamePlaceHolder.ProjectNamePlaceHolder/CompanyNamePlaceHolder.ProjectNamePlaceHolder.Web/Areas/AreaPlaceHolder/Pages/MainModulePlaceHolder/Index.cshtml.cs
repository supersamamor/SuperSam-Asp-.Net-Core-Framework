using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.Area.Projects.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Inventory.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.MainModulePlaceHolder;

[Authorize(Policy = Permission.MainModulePlaceHolder.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public MainModulePlaceHolderViewModel MainModulePlaceHolder { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetMainModulePlaceHolderQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                Template:[InsertNewJSONParameterForListingQueryTextHere]              
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetMainModulePlaceHolderQuery>(nameof(MainModulePlaceHolderState.Name)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Name }));
    }
}
