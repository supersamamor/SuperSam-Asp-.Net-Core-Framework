using CTI.DPI.Application.Features.DPI.ReportColumnFilter.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnFilter.Queries;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DPI.Web.Areas.DPI.Pages.ReportColumnFilter;

[Authorize(Policy = Permission.ReportColumnFilter.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ReportColumnFilterViewModel ReportColumnFilter { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetReportColumnFilterByIdQuery(id)), ReportColumnFilter);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditReportColumnFilterCommand>(ReportColumnFilter)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", ReportColumnFilter);
    }
	
}
