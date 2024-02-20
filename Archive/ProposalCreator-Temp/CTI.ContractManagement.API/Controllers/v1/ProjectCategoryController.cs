using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectCategory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectCategory.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectCategoryController : BaseApiController<ProjectCategoryController>
{
    [Authorize(Policy = Permission.ProjectCategory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectCategoryState>>> GetAsync([FromQuery] GetProjectCategoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ProjectCategory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectCategoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectCategoryByIdQuery(id)));

    [Authorize(Policy = Permission.ProjectCategory.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectCategoryState>> PostAsync([FromBody] ProjectCategoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectCategoryCommand>(request)));

    [Authorize(Policy = Permission.ProjectCategory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectCategoryState>> PutAsync(string id, [FromBody] ProjectCategoryViewModel request)
    {
        var command = Mapper.Map<EditProjectCategoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ProjectCategory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectCategoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectCategoryCommand { Id = id }));
}

public record ProjectCategoryViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
