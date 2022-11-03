using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.ActivityHistory.Commands;
using CTI.ELMS.Application.Features.ELMS.ActivityHistory.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class ActivityHistoryController : BaseApiController<ActivityHistoryController>
{
    [Authorize(Policy = Permission.ActivityHistory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ActivityHistoryState>>> GetAsync([FromQuery] GetActivityHistoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ActivityHistory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityHistoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetActivityHistoryByIdQuery(id)));

    [Authorize(Policy = Permission.ActivityHistory.Create)]
    [HttpPost]
    public async Task<ActionResult<ActivityHistoryState>> PostAsync([FromBody] ActivityHistoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddActivityHistoryCommand>(request)));

    [Authorize(Policy = Permission.ActivityHistory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ActivityHistoryState>> PutAsync(string id, [FromBody] ActivityHistoryViewModel request)
    {
        var command = Mapper.Map<EditActivityHistoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ActivityHistory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ActivityHistoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteActivityHistoryCommand { Id = id }));
}

public record ActivityHistoryViewModel
{
    
	public string? ActivityID { get;set; }
	
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
