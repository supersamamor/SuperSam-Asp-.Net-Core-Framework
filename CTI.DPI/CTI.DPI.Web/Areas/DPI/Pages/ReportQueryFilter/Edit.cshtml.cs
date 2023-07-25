using CTI.DPI.Application.Features.DPI.ReportQueryFilter.Commands;
using CTI.DPI.Application.Features.DPI.ReportQueryFilter.Queries;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DPI.Web.Areas.DPI.Pages.ReportQueryFilter;

[Authorize(Policy = Permission.ReportQueryFilter.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ReportQueryFilterViewModel ReportQueryFilter { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetReportQueryFilterByIdQuery(id)), ReportQueryFilter);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditReportQueryFilterCommand>(ReportQueryFilter)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", ReportQueryFilter);
    }
	
}
