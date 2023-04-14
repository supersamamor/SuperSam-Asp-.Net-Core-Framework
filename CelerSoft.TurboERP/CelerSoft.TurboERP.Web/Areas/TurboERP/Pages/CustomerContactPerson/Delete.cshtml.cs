using CelerSoft.TurboERP.Application.Features.TurboERP.CustomerContactPerson.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.CustomerContactPerson.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.CustomerContactPerson;

[Authorize(Policy = Permission.CustomerContactPerson.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public CustomerContactPersonViewModel CustomerContactPerson { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetCustomerContactPersonByIdQuery(id)), CustomerContactPerson);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteCustomerContactPersonCommand { Id = CustomerContactPerson.Id }), "Index");
    }
}
