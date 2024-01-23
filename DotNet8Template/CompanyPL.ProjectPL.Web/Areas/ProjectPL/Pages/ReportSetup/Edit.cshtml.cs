using CompanyPL.ProjectPL.Application.Features.ProjectPL.Report.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.Report.Queries;
using CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.ReportSetup;

[Authorize(Policy = Permission.ReportSetup.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ReportViewModel Report { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetReportByIdQuery(id)), Report);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditReportCommand>(Report)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();		
		if (AsyncAction == "AddReportQueryFilter")
		{
			return AddReportQueryFilter();
		}
		if (AsyncAction == "RemoveReportQueryFilter")
		{
			return RemoveReportQueryFilter();
		}
		
		
        return Partial("_InputFieldsPartial", Report);
    }

	private IActionResult AddReportQueryFilter()
	{
		ModelState.Clear();
		if (Report!.ReportQueryFilterList == null) { Report!.ReportQueryFilterList = new List<ReportQueryFilterViewModel>(); }
		Report!.ReportQueryFilterList!.Add(new ReportQueryFilterViewModel() { ReportId = Report.Id });
		return Partial("_InputFieldsPartial", Report);
	}
	private IActionResult RemoveReportQueryFilter()
	{
		ModelState.Clear();
		Report.ReportQueryFilterList = Report!.ReportQueryFilterList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Report);
	}
	
}
