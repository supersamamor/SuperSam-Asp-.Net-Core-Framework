using CTI.Common.Utility.Models;
using CTI.DSF.Application.Features.DSF.TaskCompanyAssignment.Commands;
using CTI.DSF.Application.Features.DSF.TaskCompanyAssignment.Queries;
using CTI.DSF.Core.DSF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DSF.API.Controllers.v1;

[ApiVersion("1.0")]
public class TaskCompanyAssignmentController : BaseApiController<TaskCompanyAssignmentController>
{
    [Authorize(Policy = Permission.TaskCompanyAssignment.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TaskCompanyAssignmentState>>> GetAsync([FromQuery] GetTaskCompanyAssignmentQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.TaskCompanyAssignment.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskCompanyAssignmentState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTaskCompanyAssignmentByIdQuery(id)));

    [Authorize(Policy = Permission.TaskCompanyAssignment.Create)]
    [HttpPost]
    public async Task<ActionResult<TaskCompanyAssignmentState>> PostAsync([FromBody] TaskCompanyAssignmentViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTaskCompanyAssignmentCommand>(request)));

    [Authorize(Policy = Permission.TaskCompanyAssignment.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TaskCompanyAssignmentState>> PutAsync(string id, [FromBody] TaskCompanyAssignmentViewModel request)
    {
        var command = Mapper.Map<EditTaskCompanyAssignmentCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.TaskCompanyAssignment.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TaskCompanyAssignmentState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTaskCompanyAssignmentCommand { Id = id }));
}

public record TaskCompanyAssignmentViewModel
{
    [Required]
	
	public string TaskMasterId { get;set; } = "";
	[Required]
	
	public string CompanyId { get;set; } = "";
	
	public string? DepartmentId { get;set; }
	
	public string? SectionId { get;set; }
	
	public string? TeamId { get;set; }
	   
}
