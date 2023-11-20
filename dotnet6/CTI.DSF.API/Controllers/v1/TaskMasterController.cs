using CTI.Common.Utility.Models;
using CTI.DSF.Application.Features.DSF.TaskMaster.Commands;
using CTI.DSF.Application.Features.DSF.TaskMaster.Queries;
using CTI.DSF.Core.DSF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DSF.API.Controllers.v1;

[ApiVersion("1.0")]
public class TaskMasterController : BaseApiController<TaskMasterController>
{
    [Authorize(Policy = Permission.TaskMaster.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TaskMasterState>>> GetAsync([FromQuery] GetTaskMasterQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.TaskMaster.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskMasterState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTaskMasterByIdQuery(id)));

    [Authorize(Policy = Permission.TaskMaster.Create)]
    [HttpPost]
    public async Task<ActionResult<TaskMasterState>> PostAsync([FromBody] TaskMasterViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTaskMasterCommand>(request)));

    [Authorize(Policy = Permission.TaskMaster.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TaskMasterState>> PutAsync(string id, [FromBody] TaskMasterViewModel request)
    {
        var command = Mapper.Map<EditTaskMasterCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.TaskMaster.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TaskMasterState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTaskMasterCommand { Id = id }));
}

public record TaskMasterViewModel
{
    [Required]
	public int TaskNo { get;set; }
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TaskDescription { get;set; } = "";
	[Required]
	
	public string TaskClassification { get;set; } = "";
	[Required]
	
	public string TaskFrequency { get;set; } = "";
	public int? TaskDueDay { get;set; }
	public DateTime? TargetDueDate { get;set; } = DateTime.Now.Date;
	[Required]
	
	public string HolidayTag { get;set; } = "";
	public bool Active { get;set; }
	   
}
