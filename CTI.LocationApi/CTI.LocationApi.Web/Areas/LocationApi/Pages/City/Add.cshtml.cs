using CTI.LocationApi.Application.Features.LocationApi.City.Commands;
using CTI.LocationApi.Web.Areas.LocationApi.Models;
using CTI.LocationApi.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.LocationApi.Web.Areas.LocationApi.Pages.City;

[Authorize(Policy = Permission.City.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public CityViewModel City { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddCityCommand>(City)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", City);
    }
	
}
