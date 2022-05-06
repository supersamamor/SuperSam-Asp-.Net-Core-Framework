using CTI.Common.Web.Utility.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.MainModulePlaceHolder;

[Authorize(Policy = Permission.MainModulePlaceHolder.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public MainModulePlaceHolderViewModel MainModulePlaceHolder { get; set; } = new();

    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await Mediatr.Send(new GetMainModulePlaceHolderByIdQuery(id)).ToActionResult(
            e =>
            {
                Mapper.Map(e, MainModulePlaceHolder);
                return Page();
            },
            none: null);
    }
}
