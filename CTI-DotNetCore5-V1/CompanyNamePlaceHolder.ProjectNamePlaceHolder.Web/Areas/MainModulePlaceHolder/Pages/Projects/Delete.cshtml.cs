using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.MainModulePlaceHolder.Projects.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.MainModulePlaceHolder.Projects.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.MainModulePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Commands.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.MainModulePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.MainModulePlaceHolder.Pages.Projects
{
    [Authorize(Policy = Permission.Projects.Delete)]
    public class DeleteModel : BasePageModel<DeleteModel>
    {
        [BindProperty]
        public ProjectViewModel? Project { get; set; }

        [ViewData]
        public string? Message { get; set; }

        public async Task<IActionResult> OnGetDeleteAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return await Mediatr.Send(new GetProjectByIdQuery(id))
                                .ToActionResult(p =>
                                {
                                    Message = Localizer[$"Are you sure you want to delete this project?"];
                                    return Partial("_DeleteProjectDetails",
                                                   Mapper.Map<ProjectViewModel>(p));
                                }, none: null);
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (!ModelState.IsValid)
            {
                return Partial("_DeleteProjectDetails", Project);
            }
            var result = await TryAsync(async () => await Mediatr.Send(new DeleteProjectCommand(Project!.Id!)))
                .IfFail(ex =>
                {
                    Logger.LogError(ex, "Exception in OnPostDeleteAsync");
                    return Fail<Error, Project>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
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
            return Partial("_DeleteProjectDetails", Project);
        }
    }
}
