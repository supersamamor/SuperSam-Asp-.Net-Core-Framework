using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    [Authorize(Policy = Permission.Apis.View)]
    public class ViewModel : BasePageModel<ViewModel>
    {
        private readonly OpenIddictScopeManager<OpenIddictEntityFrameworkCoreScope> _manager;

        public ViewModel(OpenIddictScopeManager<OpenIddictEntityFrameworkCoreScope> manager)
        {
            _manager = manager;
        }

        [BindProperty]
        public ScopeViewModel? Scope { get; set; }

        public async Task<IActionResult> OnGetDetailsAsync(string? name)
        {
            if (name == null)
            {
                return NotFound();
            }
            return await Optional(await _manager.FindByNameAsync(name))
                .ToActionResult(async scope =>
                {
                    var descriptor = new OpenIddictScopeDescriptor();
                    await _manager.PopulateAsync(descriptor, scope!);
                    Scope = new()
                    {
                        Name = descriptor!.Name,
                        DisplayName = descriptor.DisplayName,
                        Resources = string.Join(" ", descriptor.Resources)
                    };
                    return Page();
                }, none: null);
        }
    }
}
