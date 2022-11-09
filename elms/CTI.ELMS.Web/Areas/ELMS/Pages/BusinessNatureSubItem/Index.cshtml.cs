using CTI.ELMS.Application.Features.ELMS.BusinessNatureSubItem.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.BusinessNatureSubItem;

[Authorize(Policy = Permission.BusinessNatureSubItem.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public BusinessNatureSubItemViewModel BusinessNatureSubItem { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetBusinessNatureSubItemQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.BusinessNatureSubItemName,
				BusinessNatureID = e.BusinessNature?.BusinessNatureCode,
				IsDisabled =  e.IsDisabled == true ? "Yes" : "No",
				e.BusinessNatureSubItemCode,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetBusinessNatureSubItemQuery>(nameof(BusinessNatureSubItemState.BusinessNatureSubItemName)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.BusinessNatureSubItemName }));
    }
    public async Task<IActionResult> OnGetDropdownBusinessNatureSubItemAsync(string businessNature)
    {
        var list = new List<SelectListItem>();
        if (!string.IsNullOrEmpty(businessNature))
        {
            var businessNatureSubItemList = (await Mediatr.Send(new GetBusinessNatureSubItemQuery() { BusinessNatureId = businessNature })).Data.ToList();
            foreach (var p in businessNatureSubItemList)
                list.Add(new SelectListItem { Value = p.Id, Text = p.BusinessNatureSubItemName });
        }       
        return new JsonResult(list);
    }
}
