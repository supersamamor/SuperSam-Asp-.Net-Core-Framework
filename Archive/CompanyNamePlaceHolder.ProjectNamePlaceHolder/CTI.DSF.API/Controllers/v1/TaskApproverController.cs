using CTI.Common.Utility.Models;
using CTI.DSF.Application.Features.DSF.TaskApprover.Commands;
using CTI.DSF.Application.Features.DSF.TaskApprover.Queries;
using CTI.DSF.Core.DSF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DSF.API.Controllers.v1;

[ApiVersion("1.0")]
public class TaskApproverController : BaseApiController<TaskApproverController>
{
    [Authorize(Policy = Permission.TaskApprover.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TaskApproverState>>> GetAsync([FromQuery] GetTaskApproverQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.TaskApprover.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskApproverState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTaskApproverByIdQuery(id)));

    [Authorize(Policy = Permission.TaskApprover.Create)]
    [HttpPost]
    public async Task<ActionResult<TaskApproverState>> PostAsync([FromBody] TaskApproverViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTaskApproverCommand>(request)));

    [Authorize(Policy = Permission.TaskApprover.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TaskApproverState>> PutAsync(string id, [FromBody] TaskApproverViewModel request)
    {
        var command = Mapper.Map<EditTaskApproverCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.TaskApprover.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TaskApproverState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTaskApproverCommand { Id = id }));
}

public record TaskApproverViewModel
{
    [Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ApproverUserId { get;set; } = "";
	[Required]
	
	public string TaskCompanyAssignmentId { get;set; } = "";
	[Required]
	
	public string ApproverType { get;set; } = "";
	public bool IsPrimary { get;set; }
	[Required]
	public int Sequence { get;set; }
	   
}
