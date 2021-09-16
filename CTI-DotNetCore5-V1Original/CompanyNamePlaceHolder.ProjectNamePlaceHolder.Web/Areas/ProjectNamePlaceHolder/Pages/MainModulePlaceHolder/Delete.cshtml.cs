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
    [Authorize(Policy = Permission.MainModulePlaceHolder.Delete)]
    public class DeleteModel : BasePageModel<DeleteModel>
    {
        [BindProperty]
        public MainModulePlaceHolderViewModel? MainModulePlaceHolder { get; set; }

        [ViewData]
        public string? Message { get; set; }

        public async Task<IActionResult> OnGetDeleteAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return await Mediatr.Send(new GetMainModulePlaceHolderByIdQuery(id))
                                .ToActionResult(p =>
                                {
                                    Message = Localizer[$"Are you sure you want to delete this MainModulePlaceHolder?"];
                                    return Partial("_DeleteMainModulePlaceHolderDetails",
                                                   Mapper.Map<MainModulePlaceHolderViewModel>(p));
                                }, none: null);
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (!ModelState.IsValid)
            {
                return Partial("_DeleteMainModulePlaceHolderDetails", MainModulePlaceHolder);
            }
            var result = await TryAsync(async () => await Mediatr.Send(new DeleteMainModulePlaceHolderCommand(MainModulePlaceHolder!.Id!)))
                .IfFail(ex =>
                {
                    Logger.LogError(ex, "Exception in OnPostDeleteAsync");
                    return Fail<Error, Core.ProjectNamePlaceHolder.MainModulePlaceHolder>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                });
            result.Match(
                Succ: succ =>
                {
                    NotyfService.Success(Localizer["Record deleted successfully"]);
                    Logger.LogInformation("Deleted Record. ID: {ID}, Record: {Record}", succ.Id, succ.ToString());
                },
                Fail: errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    Logger.LogError("Error in OnPostDeleteAsync. Errors: {Errors}", string.Join(",", errors));
                });
            return Partial("_DeleteMainModulePlaceHolderDetails", MainModulePlaceHolder);
        }
    }
}
