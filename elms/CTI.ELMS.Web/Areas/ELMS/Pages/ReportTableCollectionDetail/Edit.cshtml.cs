using CTI.ELMS.Application.Features.ELMS.ReportTableCollectionDetail.Commands;
using CTI.ELMS.Application.Features.ELMS.ReportTableCollectionDetail.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.ReportTableCollectionDetail;

[Authorize(Policy = Permission.ReportTableCollectionDetail.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ReportTableCollectionDetailViewModel ReportTableCollectionDetail { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await PageFrom(async () => await Mediatr.Send(new GetReportTableCollectionDetailByIdQuery(id)), ReportTableCollectionDetail);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditReportTableCollectionDetailCommand>(ReportTableCollectionDetail)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", ReportTableCollectionDetail);
    }
	
}
