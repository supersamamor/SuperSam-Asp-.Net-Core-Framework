using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.ProjectNamePlaceHolder.MainModulePlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.MainModulePlaceHolder
{
    [Authorize(Policy = Permission.MainModulePlaceHolder.Edit)]
    public class EditModel : BasePageModel<EditModel>
    {
        [BindProperty]
        public MainModulePlaceHolderViewModel? MainModulePlaceHolder { get; set; }

        public async Task<IActionResult> OnGetDetailsAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return await Mediatr.Send(new GetMainModulePlaceHolderByIdQuery(id))
                                .ToActionResult(e => Partial("_EditMainModulePlaceHolderDetails",
                                                             Mapper.Map<MainModulePlaceHolderViewModel>(e)), none: null);
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                return Partial("_EditMainModulePlaceHolderDetails", MainModulePlaceHolder);
            }
            var result = await TryAsync(async () => await Mediatr.Send(Mapper.Map<EditMainModulePlaceHolderCommand>(MainModulePlaceHolder)))
                .IfFail(ex =>
                {
                    Logger.LogError(ex, "Exception in OnPostEditAsync");
                    return Fail<Error, Core.ProjectNamePlaceHolder.MainModulePlaceHolder>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                });
            result.Match(
                Succ: succ =>
                {
                    NotyfService.Success(Localizer["Record saved successfully"]);
                    Logger.LogInformation("Edited Record. ID: {ID}, Record: {Record}", succ.Id, succ.ToString());
                },
                Fail: errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    Logger.LogError("Error in OnPostEditAsync. Errors: {Errors}", string.Join(",", errors));
                });
            return Partial("_EditMainModulePlaceHolderDetails", MainModulePlaceHolder);
        }
    }
}
