using CTI.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Projects.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Projects.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectsController : BaseApiController<ProjectsController>
{
    [Authorize(Policy = Permission.Projects.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectState>>> GetAsync([FromQuery] GetProjectsQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Projects.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectByIdQuery(id)));

    [Authorize(Policy = Permission.Projects.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectState>> PostAsync([FromBody] ProjectViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectCommand>(request)));

    [Authorize(Policy = Permission.Projects.Edit)]
    [HttpPut]
    public async Task<ActionResult<ProjectState>> PutAsync([FromBody] ProjectViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<EditProjectCommand>(request)));

    [Authorize(Policy = Permission.Projects.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectCommand { Id = id }));
}

public record ProjectViewModel
{
    [Required]
    public string Id { get; set; } = "";
    [Required]
    public string Code { get; set; } = "";
    [Required]
    public string Status { get; init; } = "";
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public string Description { get; init; } = "";
}
