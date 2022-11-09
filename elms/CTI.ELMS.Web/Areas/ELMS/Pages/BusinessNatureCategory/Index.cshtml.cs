using CTI.ELMS.Application.Features.ELMS.BusinessNatureCategory.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.BusinessNatureCategory;

[Authorize(Policy = Permission.BusinessNatureCategory.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public BusinessNatureCategoryViewModel BusinessNatureCategory { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetBusinessNatureCategoryQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.BusinessNatureCategoryCode,
				e.BusinessNatureCategoryName,
				BusinessNatureSubItemID = e.BusinessNatureSubItem?.Id,
				IsDisabled =  e.IsDisabled == true ? "Yes" : "No",
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetBusinessNatureCategoryQuery>(nameof(BusinessNatureCategoryState.BusinessNatureCategoryName)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.BusinessNatureCategoryName }));
    }
    public async Task<IActionResult> OnGetDropdownBusinessNatureCategoryAsync(string? businessNatureSubItem)
    {
        var list = new List<SelectListItem>();
        if (!string.IsNullOrEmpty(businessNatureSubItem))
        {
            var businessNatureCategoryList = (await Mediatr.Send(new GetBusinessNatureCategoryQuery() { BusinessNatureSubItemId = businessNatureSubItem })).Data.ToList();
            foreach (var p in businessNatureCategoryList)
                list.Add(new SelectListItem { Value = p.Id, Text = p.BusinessNatureCategoryName });
        }   
        return new JsonResult(list);
    }
}
