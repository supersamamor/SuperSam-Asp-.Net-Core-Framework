using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.UserProjectAssignment.Commands;
using CTI.ELMS.Application.Features.ELMS.UserProjectAssignment.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class UserProjectAssignmentController : BaseApiController<UserProjectAssignmentController>
{
    [Authorize(Policy = Permission.UserProjectAssignment.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<UserProjectAssignmentState>>> GetAsync([FromQuery] GetUserProjectAssignmentQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.UserProjectAssignment.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserProjectAssignmentState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetUserProjectAssignmentByIdQuery(id)));

    [Authorize(Policy = Permission.UserProjectAssignment.Create)]
    [HttpPost]
    public async Task<ActionResult<UserProjectAssignmentState>> PostAsync([FromBody] UserProjectAssignmentViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddUserProjectAssignmentCommand>(request)));

    [Authorize(Policy = Permission.UserProjectAssignment.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UserProjectAssignmentState>> PutAsync(string id, [FromBody] UserProjectAssignmentViewModel request)
    {
        var command = Mapper.Map<EditUserProjectAssignmentCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.UserProjectAssignment.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<UserProjectAssignmentState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteUserProjectAssignmentCommand { Id = id }));
}

public record UserProjectAssignmentViewModel
{
    
	public string? UserId { get;set; }
	
	public string? ProjectID { get;set; }
	   
}
