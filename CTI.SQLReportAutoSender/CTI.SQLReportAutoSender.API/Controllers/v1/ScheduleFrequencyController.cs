using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequency.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequency.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.SQLReportAutoSender.API.Controllers.v1;

[ApiVersion("1.0")]
public class ScheduleFrequencyController : BaseApiController<ScheduleFrequencyController>
{
    [Authorize(Policy = Permission.ScheduleFrequency.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ScheduleFrequencyState>>> GetAsync([FromQuery] GetScheduleFrequencyQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ScheduleFrequency.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleFrequencyState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetScheduleFrequencyByIdQuery(id)));

    [Authorize(Policy = Permission.ScheduleFrequency.Create)]
    [HttpPost]
    public async Task<ActionResult<ScheduleFrequencyState>> PostAsync([FromBody] ScheduleFrequencyViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddScheduleFrequencyCommand>(request)));

    [Authorize(Policy = Permission.ScheduleFrequency.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ScheduleFrequencyState>> PutAsync(string id, [FromBody] ScheduleFrequencyViewModel request)
    {
        var command = Mapper.Map<EditScheduleFrequencyCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ScheduleFrequency.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ScheduleFrequencyState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteScheduleFrequencyCommand { Id = id }));
}

public record ScheduleFrequencyViewModel
{
    [Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get;set; } = "";
	   
}
