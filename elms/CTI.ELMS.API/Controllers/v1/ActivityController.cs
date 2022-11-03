using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.Activity.Commands;
using CTI.ELMS.Application.Features.ELMS.Activity.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class ActivityController : BaseApiController<ActivityController>
{
    [Authorize(Policy = Permission.Activity.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ActivityState>>> GetAsync([FromQuery] GetActivityQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Activity.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetActivityByIdQuery(id)));

    [Authorize(Policy = Permission.Activity.Create)]
    [HttpPost]
    public async Task<ActionResult<ActivityState>> PostAsync([FromBody] ActivityViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddActivityCommand>(request)));

    [Authorize(Policy = Permission.Activity.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ActivityState>> PutAsync(string id, [FromBody] ActivityViewModel request)
    {
        var command = Mapper.Map<EditActivityCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Activity.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ActivityState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteActivityCommand { Id = id }));
}

public record ActivityViewModel
{
    
	public string? LeadID { get;set; }
	
	public string? ProjectID { get;set; }
	
	public string? LeadTaskId { get;set; }
	public DateTime? ActivityDate { get;set; } = DateTime.Now.Date;
	
	public string? ClientFeedbackId { get;set; }
	[Required]
	
	public string NextStepId { get;set; } = "";
	[Required]
	public DateTime TargetDate { get;set; } = DateTime.Now.Date;
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ActivityRemarks { get;set; } = "";
	[Required]
	public DateTime PCTDate { get;set; } = DateTime.Now.Date;
	[Required]
	
	public string UnitsInformation { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get;set; } = "";
	   
}
