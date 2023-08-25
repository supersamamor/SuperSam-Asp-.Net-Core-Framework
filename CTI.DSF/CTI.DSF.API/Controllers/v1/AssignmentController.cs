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
    [Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AssignmentCode { get;set; } = "";
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TaskListId { get;set; } = "";
	[StringLength(36, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PrimaryAssignee { get;set; }
	[Required]
	[StringLength(36, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AlternateAssignee { get;set; } = "";
	public DateTime? StartDate { get;set; } = DateTime.Now.Date;
	public DateTime? EndDate { get;set; } = DateTime.Now.Date;
	   
}
