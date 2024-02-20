using CTI.Common.Utility.Models;
using CTI.DSF.Application.Features.DSF.TaskTag.Commands;
using CTI.DSF.Application.Features.DSF.TaskTag.Queries;
using CTI.DSF.Core.DSF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DSF.API.Controllers.v1;

[ApiVersion("1.0")]
public class TaskTagController : BaseApiController<TaskTagController>
{
    [Authorize(Policy = Permission.TaskTag.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TaskTagState>>> GetAsync([FromQuery] GetTaskTagQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.TaskTag.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskTagState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTaskTagByIdQuery(id)));

    [Authorize(Policy = Permission.TaskTag.Create)]
    [HttpPost]
    public async Task<ActionResult<TaskTagState>> PostAsync([FromBody] TaskTagViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTaskTagCommand>(request)));

    [Authorize(Policy = Permission.TaskTag.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TaskTagState>> PutAsync(string id, [FromBody] TaskTagViewModel request)
    {
        var command = Mapper.Map<EditTaskTagCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.TaskTag.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TaskTagState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTaskTagCommand { Id = id }));
}

public record TaskTagViewModel
{
    [Required]
	
	public string TaskMasterId { get;set; } = "";
	[Required]
	
	public string TagId { get;set; } = "";
	   
}
