using CTI.DPI.Application.Features.DPI.ReportColumnDetail.Commands;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DPI.Web.Areas.DPI.Pages.ReportColumnDetail;

[Authorize(Policy = Permission.ReportColumnDetail.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ReportColumnDetailViewModel ReportColumnDetail { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public IActionResult OnGet()
    {
		
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddReportColumnDetailCommand>(ReportColumnDetail)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", ReportColumnDetail);
    }
	
}
