using CTI.DSF.Application.Features.DSF.Section.Commands;
using CTI.DSF.Application.Features.DSF.Section.Queries;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.Section;

[Authorize(Policy = Permission.Section.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public SectionViewModel Section { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetSectionByIdQuery(id)), Section);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditSectionCommand>(Section)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", Section);
    }
	
}
