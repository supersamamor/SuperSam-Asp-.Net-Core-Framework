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
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Applications
{
    [Authorize(Policy = Permission.Applications.View)]
    public class ViewModel : BasePageModel<ViewModel>
    {
        private readonly OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> _manager;

        public ViewModel(OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> manager)
        {
            _manager = manager;
        }

        [BindProperty]
        public ApplicationViewModel? Application { get; set; }

        public async Task<IActionResult> OnGetDetailsAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return await Optional(await _manager.FindByClientIdAsync(id))
                .ToActionResult(async application =>
                {
                    var descriptor = new OpenIddictApplicationDescriptor();
                    await _manager.PopulateAsync(descriptor, application!);
                    Application = new()
                    {
                        ClientId = descriptor!.ClientId!,
                        DisplayName = descriptor.DisplayName,
                        RedirectUri = string.Join(" ", descriptor.RedirectUris),
                        Scopes = string.Join(" ",
                                             descriptor.Permissions.Where(p => p.StartsWith(Permissions.Prefixes.Scope))
                                                                   .Map(p => p[4..]))
                    };
                    return Page();
                }, none: null);
        }
    }
}
