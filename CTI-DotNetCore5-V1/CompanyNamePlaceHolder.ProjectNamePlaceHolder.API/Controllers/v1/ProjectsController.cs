using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.MainModulePlaceHolder.Projects.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.MainModulePlaceHolder.Projects.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.MainModulePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OpenIddict.Validation.AspNetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProjectsController : BaseApiController<ProjectsController>
    {
        [Authorize(Policy = Permission.Projects.View)]
        [ProducesResponseType(typeof(PagedListResponse<Project>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetProjectsQuery query) =>
            Ok(await Mediator.Send(query));

        [Authorize(Policy = Permission.Projects.View)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id) =>
            await Mediator.Send(new GetProjectByIdQuery(id)).ToActionResult();

        [Authorize(Policy = Permission.Projects.Create)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProjectViewModel request) =>
            await Mediator.Send(Mapper.Map<AddProjectCommand>(request)).ToActionResult(
                success: null,
                errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    return BadRequest(ModelState);
                });

        [Authorize(Policy = Permission.Projects.Edit)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] ProjectViewModel request) =>
            await Mediator.Send(Mapper.Map<EditProjectCommand>(request)).ToActionResult(
                success: null,
                errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    return BadRequest(ModelState);
                });

        [Authorize(Policy = Permission.Projects.Delete)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id) =>
            await Mediator.Send(new DeleteProjectCommand(id)).ToActionResult(
                success: null,
                errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    return BadRequest(ModelState);
                });
    }

    public record ProjectViewModel
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public string? Status { get; set; }
    }
}
