using CompanyPL.Common.Web.Utility.Extensions;
using CompanyPL.ProjectPL.Web.Areas.Admin.Models;
using CompanyPL.ProjectPL.Web.Models;
using CompanyPL.ProjectPL.Core.Oidc;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using static LanguageExt.Prelude;

namespace CompanyPL.ProjectPL.Web.Areas.Admin.Pages.Apis;

[Authorize(Policy = Permission.Apis.Create)]
public class AddModel : BasePageModel<AddModel>
{
    private readonly OpenIddictScopeManager<OidcScope> _manager;

    public AddModel(OpenIddictScopeManager<OidcScope> manager)
    {
        _manager = manager;
    }

    [BindProperty]
    public ScopeViewModel Scope { get; set; } = new();

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
        return await Optional(await _manager.FindByNameAsync(Scope.Name))
            .MatchAsync(
            scope => Fail<Error, ScopeViewModel>($"API with name {scope!.Name} already exists"),
            async () => await CreateApi())
            .ToActionResult(
            success: scope =>
            {
                NotyfService.Success(Localizer["Record saved successfully"]);
                Logger.LogInformation("Created Scope. Name: {Name}, Scope: {Scope}", scope.Name, scope.ToString());
                return RedirectToPage("View", new { name = Scope.Name });
            },
            fail: errors =>
            {
                errors.Iter(error => ModelState.AddModelError("", error.ToString()));
                Logger.LogError("Error in OnPostAddAsync. Error: {Errors}", string.Join(",", errors));
                return Page();
            });
    }

    async Task<Validation<Error, ScopeViewModel>> CreateApi()
    {
        return await TryAsync(async () =>
        {
            await _manager.CreateAsync(new OpenIddictScopeDescriptor
            {
                DisplayName = Scope.DisplayName,
                Name = Scope.Name,
                Resources =
                    {
                        Scope.Resources
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
