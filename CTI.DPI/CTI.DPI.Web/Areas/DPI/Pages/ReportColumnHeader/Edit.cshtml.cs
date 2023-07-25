using CTI.DPI.Application.Features.DPI.ReportColumnHeader.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnHeader.Queries;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DPI.Web.Areas.DPI.Pages.ReportColumnHeader;

[Authorize(Policy = Permission.ReportColumnHeader.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ReportColumnHeaderViewModel ReportColumnHeader { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetReportColumnHeaderByIdQuery(id)), ReportColumnHeader);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditReportColumnHeaderCommand>(ReportColumnHeader)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddReportColumnDetail")
		{
			return AddReportColumnDetail();
		}
		if (AsyncAction == "RemoveReportColumnDetail")
		{
			return RemoveReportColumnDetail();
		}
		
		
        return Partial("_InputFieldsPartial", ReportColumnHeader);
    }
	
	private IActionResult AddReportColumnDetail()
	{
		ModelState.Clear();
		if (ReportColumnHeader!.ReportColumnDetailList == null) { ReportColumnHeader!.ReportColumnDetailList = new List<ReportColumnDetailViewModel>(); }
		ReportColumnHeader!.ReportColumnDetailList!.Add(new ReportColumnDetailViewModel() { ReportColumnId = ReportColumnHeader.Id });
		return Partial("_InputFieldsPartial", ReportColumnHeader);
	}
	private IActionResult RemoveReportColumnDetail()
	{
		ModelState.Clear();
		ReportColumnHeader.ReportColumnDetailList = ReportColumnHeader!.ReportColumnDetailList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ReportColumnHeader);
	}
	
}
