using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AuditTrail.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.AuditTrail
{
    [Authorize(Policy = Permission.AuditTrail.View)]
    public class DetailsModel : BasePageModel<DetailsModel>
    {
        readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public AuditLogViewModel AuditLog { get; set; } = new();

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return await Mediatr.Send(new GetAuditLogByIdQuery((int)id)).ToActionResult(
                someAsync: async e =>
                {
                    Mapper.Map(e, AuditLog);
                    var user = await _userManager.FindByIdAsync(e.UserId);
                    AuditLog.User = Mapper.Map<AuditLogUserViewModel>(user);
                    return Page();
                },
                none: null);
        }
    }
}