using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.CustomSchedule.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.CustomSchedule.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.SQLReportAutoSender.API.Controllers.v1;

[ApiVersion("1.0")]
public class CustomScheduleController : BaseApiController<CustomScheduleController>
{
    [Authorize(Policy = Permission.CustomSchedule.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<CustomScheduleState>>> GetAsync([FromQuery] GetCustomScheduleQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.CustomSchedule.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomScheduleState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetCustomScheduleByIdQuery(id)));

    [Authorize(Policy = Permission.CustomSchedule.Create)]
    [HttpPost]
    public async Task<ActionResult<CustomScheduleState>> PostAsync([FromBody] CustomScheduleViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddCustomScheduleCommand>(request)));

    [Authorize(Policy = Permission.CustomSchedule.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CustomScheduleState>> PutAsync(string id, [FromBody] CustomScheduleViewModel request)
    {
        var command = Mapper.Map<EditCustomScheduleCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.CustomSchedule.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<CustomScheduleState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteCustomScheduleCommand { Id = id }));
}

public record CustomScheduleViewModel
{
    [Required]
	
	public string ReportId { get;set; } = "";
	[Required]
	public int SequenceNumber { get;set; }
	[Required]
	public DateTime DateTimeSchedule { get;set; } = DateTime.Now.Date;
	   
}
