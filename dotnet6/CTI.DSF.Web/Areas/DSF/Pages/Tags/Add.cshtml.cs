using CTI.DSF.Application.Features.DSF.Tags.Commands;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.Tags;

[Authorize(Policy = Permission.Tags.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public TagsViewModel Tags { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddTagsCommand>(Tags)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddTaskTag")
		{
			return AddTaskTag();
		}
		if (AsyncAction == "RemoveTaskTag")
		{
			return RemoveTaskTag();
		}
		
		
        return Partial("_InputFieldsPartial", Tags);
    }
	
	private IActionResult AddTaskTag()
	{
		ModelState.Clear();
		if (Tags!.TaskTagList == null) { Tags!.TaskTagList = new List<TaskTagViewModel>(); }
		Tags!.TaskTagList!.Add(new TaskTagViewModel() { TagId = Tags.Id });
		return Partial("_InputFieldsPartial", Tags);
	}
	private IActionResult RemoveTaskTag()
	{
		ModelState.Clear();
		Tags.TaskTagList = Tags!.TaskTagList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Tags);
	}
	
}
