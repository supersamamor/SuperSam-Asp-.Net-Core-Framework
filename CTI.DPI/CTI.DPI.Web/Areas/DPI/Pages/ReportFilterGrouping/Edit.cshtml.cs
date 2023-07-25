using CTI.DPI.Application.Features.DPI.ReportFilterGrouping.Commands;
using CTI.DPI.Application.Features.DPI.ReportFilterGrouping.Queries;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DPI.Web.Areas.DPI.Pages.ReportFilterGrouping;

[Authorize(Policy = Permission.ReportFilterGrouping.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ReportFilterGroupingViewModel ReportFilterGrouping { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetReportFilterGroupingByIdQuery(id)), ReportFilterGrouping);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditReportFilterGroupingCommand>(ReportFilterGrouping)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddReportColumnFilter")
		{
			return AddReportColumnFilter();
		}
		if (AsyncAction == "RemoveReportColumnFilter")
		{
			return RemoveReportColumnFilter();
		}
		
		
        return Partial("_InputFieldsPartial", ReportFilterGrouping);
    }
	
	private IActionResult AddReportColumnFilter()
	{
		ModelState.Clear();
		if (ReportFilterGrouping!.ReportColumnFilterList == null) { ReportFilterGrouping!.ReportColumnFilterList = new List<ReportColumnFilterViewModel>(); }
		ReportFilterGrouping!.ReportColumnFilterList!.Add(new ReportColumnFilterViewModel() { ReportFilterGroupingId = ReportFilterGrouping.Id });
		return Partial("_InputFieldsPartial", ReportFilterGrouping);
	}
	private IActionResult RemoveReportColumnFilter()
	{
		ModelState.Clear();
		ReportFilterGrouping.ReportColumnFilterList = ReportFilterGrouping!.ReportColumnFilterList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ReportFilterGrouping);
	}
	
}
