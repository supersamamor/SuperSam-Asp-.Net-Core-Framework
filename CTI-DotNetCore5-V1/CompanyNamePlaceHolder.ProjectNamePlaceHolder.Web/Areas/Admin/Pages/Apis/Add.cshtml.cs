using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Apis
{
    [Authorize(Policy = Permission.Apis.Create)]
    public class AddModel : BasePageModel<AddModel>
    {
        private readonly OpenIddictScopeManager<OpenIddictEntityFrameworkCoreScope> _manager;

        public AddModel(OpenIddictScopeManager<OpenIddictEntityFrameworkCoreScope> manager)
        {
            _manager = manager;
        }

        [BindProperty]
        public ScopeViewModel? Scope { get; set; }

        public IActionResult OnGetAdd()
        {
            return Partial("_AddApi", new ScopeViewModel());
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                return Partial("_AddApi", Scope);
            }
            var result = await Optional(await _manager.FindByNameAsync(Scope!.Name!))
                .MatchAsync(
                scope => Fail<Error, ScopeViewModel>($"API with name {scope!.Name} already exists"),
                async () => await CreateApi()
                );
            result.Match(
                Succ: scope =>
                {
                    NotyfService.Success(Localizer["Record saved successfully"]);
                    Logger.LogInformation("Created Scope. Name: {Name}, Scope: {Scope}", scope.Name, scope.ToString());
                },
                Fail: errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    Logger.LogError("Error in OnPostAddAsync. Error: {Errors}", string.Join(",", errors));
                });
            return Partial("_AddApi", Scope);
        }

        async Task<Validation<Error, ScopeViewModel>> CreateApi()
        {
            return await TryAsync(async () =>
            {
                await _manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    DisplayName = Scope!.DisplayName,
                    Name = Scope.Name,
                    Resources =
                        {
                            Scope.Resources!
                        }
                });
                return Success<Error, ScopeViewModel>(Scope);
            }).IfFail(ex =>
            {
                Logger.LogError(ex, "Exception in OnPostAddAsync");
                return Fail<Error, ScopeViewModel>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
            });
        }
    }
}
