using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Web.Areas.Admin.Queries.Users;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.TenantPOSSales;

[Authorize(Policy = Permission.TenantPOSSales.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    private readonly int _hoursOffset;
    public DetailsModel(IConfiguration configuration)
    {
        _hoursOffset = configuration.GetValue<int>("HoursOffset");
    }
    public TenantPOSSalesViewModel TenantPOSSales { get; set; } = new();
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
        return await PageFromWithUpdatedDateAndBy(async () => await Mediatr.Send(new GetTenantPOSSalesByIdQuery(id)), TenantPOSSales);
    }
    private async Task<IActionResult> PageFromWithUpdatedDateAndBy(Func<Task<Option<TenantPOSSalesState>>> f, TenantPOSSalesViewModel model) =>
    await f().ToActionResult(
       e =>
       {
           Mapper.Map(e, model);
           var user = Mediatr.Send(new GetUserByIdQuery(e.LastModifiedBy!)).Result;
           if (user == null)
           { model.UpdatedByName = "System"; }
           else
           { _ = user.Select(l => model.UpdatedByName = l.Name); }
           model.UpdatedDate = e.LastModifiedDate.AddHours(_hoursOffset).ToString("MMMM dd, yyyy hh:mm:ss tt");
           return Page();
       },
   none: null);
}
