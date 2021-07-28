using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.MainModulePlaceHolder
{
    [Authorize(Policy = Permission.MainModulePlaceHolder.Create)]
    public class AddModel : BasePageModel<AddModel>
    {
        [BindProperty]
        public MainModulePlaceHolderViewModel? MainModulePlaceHolder { get; set; }

        public IActionResult OnGetAdd()
        {
            return Partial("_AddMainModulePlaceHolderDetails", new MainModulePlaceHolderViewModel());
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                return Partial("_AddMainModulePlaceHolderDetails", MainModulePlaceHolder);
            }
            var result = await TryAsync(async () => await Mediatr.Send(Mapper.Map<AddMainModulePlaceHolderCommand>(MainModulePlaceHolder)))
                .IfFail(ex =>
                {
                    Logger.LogError(ex, "Exception in OnPostAddAsync");
                    return Fail<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                });
            result.Match(
                Succ: succ =>
                {
                    NotyfService.Success(Localizer["Record saved successfully"]);
                    Logger.LogInformation("Added Record. ID: {ID}, Record: {Record}", succ.Id, succ.ToString());
                },
                Fail: errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    Logger.LogError("Error in OnPostAddAsync. Errors: {Errors}", string.Join(",", errors));
                });
            return Partial("_AddMainModulePlaceHolderDetails", MainModulePlaceHolder);
        }
    }
}
