using CTI.LocationApi.Application.Features.LocationApi.Country.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Country.Queries;
using CTI.LocationApi.Web.Areas.LocationApi.Models;
using CTI.LocationApi.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.LocationApi.Web.Areas.LocationApi.Pages.Country;

[Authorize(Policy = Permission.Country.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public CountryViewModel Country { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetCountryByIdQuery(id)), Country);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditCountryCommand>(Country)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", Country);
    }
	
}
