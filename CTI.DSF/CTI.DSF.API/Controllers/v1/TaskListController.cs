using CTI.Common.Utility.Models;
using CTI.DSF.Application.Features.DSF.TaskList.Commands;
using CTI.DSF.Application.Features.DSF.TaskList.Queries;
using CTI.DSF.Core.DSF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DSF.API.Controllers.v1;

[ApiVersion("1.0")]
public class TaskListController : BaseApiController<TaskListController>
{
    [Authorize(Policy = Permission.TaskList.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TaskListState>>> GetAsync([FromQuery] GetTaskListQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.TaskList.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskListState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTaskListByIdQuery(id)));

    [Authorize(Policy = Permission.TaskList.Create)]
    [HttpPost]
    public async Task<ActionResult<TaskListState>> PostAsync([FromBody] TaskListViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTaskListCommand>(request)));

    [Authorize(Policy = Permission.TaskList.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TaskListState>> PutAsync(string id, [FromBody] TaskListViewModel request)
    {
        var command = Mapper.Map<EditTaskListCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.TaskList.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TaskListState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTaskListCommand { Id = id }));
}

public record TaskListViewModel
{
    
	public string? TaskListCode { get;set; }
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TaskDescription { get;set; } = "";
	[Required]
	
	public string TaskType { get;set; } = "";
	[Required]
	
	public string TaskFrequency { get;set; } = "";
	[Required]
	public int TaskDueDay { get;set; }
	[Required]
	public DateTime TargetDueDate { get;set; } = DateTime.Now.Date;
	
	public string? PrimaryEndorser { get;set; }
	[Required]
	
	public string PrimaryApprover { get;set; } = "";
	
	public string? AlternateEndorser { get;set; }
	[Required]
	
	public string AlternateApprover { get;set; } = "";
	   
}
