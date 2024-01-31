using CompanyPL.ProjectPL.Application.Features.ProjectPL.Report.Commands;
using CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.ReportSetup;

[Authorize(Policy = Permission.ReportSetup.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ReportViewModel Report { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddReportCommand>(Report)), "Details", true);
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
