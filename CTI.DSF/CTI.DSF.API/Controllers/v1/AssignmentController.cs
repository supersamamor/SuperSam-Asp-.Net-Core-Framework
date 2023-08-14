using CTI.Common.Utility.Models;
using CTI.DSF.Application.Features.DSF.Assignment.Commands;
using CTI.DSF.Application.Features.DSF.Assignment.Queries;
using CTI.DSF.Core.DSF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DSF.API.Controllers.v1;

[ApiVersion("1.0")]
public class AssignmentController : BaseApiController<AssignmentController>
{
    [Authorize(Policy = Permission.Assignment.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<AssignmentState>>> GetAsync([FromQuery] GetAssignmentQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Assignment.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<AssignmentState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetAssignmentByIdQuery(id)));

    [Authorize(Policy = Permission.Assignment.Create)]
    [HttpPost]
    public async Task<ActionResult<AssignmentState>> PostAsync([FromBody] AssignmentViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddAssignmentCommand>(request)));

    [Authorize(Policy = Permission.Assignment.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<AssignmentState>> PutAsync(string id, [FromBody] AssignmentViewModel request)
    {
        var command = Mapper.Map<EditAssignmentCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Assignment.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<AssignmentState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteAssignmentCommand { Id = id }));
}

public record AssignmentViewModel
{
    
	public string? AssignmentCode { get;set; }
	
	public string? TaskListCode { get;set; }
	[Required]
	
	public string PrimaryAsignee { get;set; } = "";
	
	public string? AlternateAsignee { get;set; }
	[Required]
	public DateTime StartDate { get;set; } = DateTime.Now.Date;
	[Required]
	public DateTime EndDate { get;set; } = DateTime.Now.Date;
	   
}
